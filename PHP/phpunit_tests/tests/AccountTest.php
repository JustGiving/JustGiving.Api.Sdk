<?php
include_once 'Base.php';
class AccountTest extends Base {
    public function testCreate_WhenSuppliedWithValidNewAccountDetails_CreatesAccount() {
        $uniqueId = uniqid();
        $request = new CreateAccountRequest();
        $request->email = "test" . $uniqueId . "@justgiving.com";
        $request->firstName = "first" . $uniqueId;
        $request->lastName = "last" . $uniqueId;
        $request->password = "testpassword";
        $request->title = "Mr";
        $request->address->line1 = "testLine1" . $uniqueId;
        $request->address->line2 = "testLine2" . $uniqueId;
        $request->address->country = "United Kingdom";
        $request->address->countyOrState = "testCountyOrState" . $uniqueId;
        $request->address->townOrCity = "testTownOrCity" . $uniqueId;
        $request->address->postcodeOrZipcode = "M130EJ";
        $request->acceptTermsAndConditions = true;
        $response = $this->client->Account->Create($request);
        $this->assertEquals($response->email, $request->email);
    }
    public function testListAllPages_WhenSuppliedWithAValidAccount_RetrievesPages() {
        $response = $this->client->Account->ListAllPages("apiunittest@justgiving.com");
        $this->assertTrue(count($response) > 0);
    }
    public function testIsEmailRegistered_WhenSuppliedEmailUnlikelyToExist_ReturnsFalse() {
        $booleanResponse = $this->client->Account->IsEmailRegistered(uniqid() + "@" + uniqid() + "-justgiving.com");
        $this->assertFalse($booleanResponse);
    }
    public function testIsEmailRegistered_WhenSuppliedKnownEmail_ReturnsTrue() {
        $booleanResponse = $this->client->Account->IsEmailRegistered($this->context->TestUsername);
        $this->assertTrue($booleanResponse);
    }
    public function testIsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsValid() {
        $request = new ValidateAccountRequest();
        $request->email = $this->context->TestUsername;
        $request->password = $this->context->TestValidPassword;
        $response = $this->client->Account->IsValid($request);
        $this->assertTrue($response->consumerId > 0);
        $this->assertEquals($response->isValid, 1);
    }
    public function testIsAccountValid_WhenSuppliedKnownEmailAndPassword_ReturnsInValid() {
        $request = new ValidateAccountRequest();
        $request->email = $this->context->TestUsername;
        $request->password = $this->context->TestInvalidPassword;
        $response = $this->client->Account->IsValid($request);
        $this->assertEquals($response->consumerId,0);
        $this->assertEquals($response->isValid,0);
    }
    public function testGetAccountDetails_WhenSuppliedAuthentication_RetriveAccountDetails() {
        $response = $this->client->Account->AccountDetails();
        $this->assertNotNull($response->email);
        $this->assertEquals($this->context->TestUsername, $response->email);
    }
    //test change password
    public function testChangeAccountPassword_WhenSuppliedCorrectCurrentPasswordAndNewPassword_ReturnSuccess_False() {
        $uniqueId = uniqid();
        $request = new CreateAccountRequest();
        $request->email = "test+" . $uniqueId . "@test.com";
        $request->firstName = "first" . $uniqueId;
        $request->lastName = "last" . $uniqueId;
        $request->password = "testpassword";
        $request->title = "Mr";
        $request->address->line1 = "testLine1" . $uniqueId;
        $request->address->line2 = "testLine2" . $uniqueId;
        $request->address->country = "United Kingdom";
        $request->address->countyOrState = "testCountyOrState" . $uniqueId;
        $request->address->townOrCity = "testTownOrCity" . $uniqueId;
        $request->address->postcodeOrZipcode = "M130EJ";
        $request->acceptTermsAndConditions = true;
        $response = $this->client->Account->Create($request);
        $badPassword = 'password';

        $cprequest = new ChangePasswordRequest();
        $cprequest->emailaddress = $request->email;
        $cprequest->newpassword = $badPassword;
        $cprequest->currentpassword = $badPassword;
        $response = $this->client->Account->ChangePassword($cprequest);
        $this->assertEquals($response->success, 0);
    }
    public function testChangeAccountPassword_WhenSuppliedInCorrectCurrentPasswordAndNewPassword_ReturnSuccess_True() {
        $uniqueId = uniqid();
        $request = new CreateAccountRequest();
        $request->email = "test+" . $uniqueId . "@test.com";
        $request->firstName = "first" . $uniqueId;
        $request->lastName = "last" . $uniqueId;
        $request->password = "testpassword";
        $request->title = "Mr";
        $request->address->line1 = "testLine1" . $uniqueId;
        $request->address->line2 = "testLine2" . $uniqueId;
        $request->address->country = "United Kingdom";
        $request->address->countyOrState = "testCountyOrState" . $uniqueId;
        $request->address->townOrCity = "testTownOrCity" . $uniqueId;
        $request->address->postcodeOrZipcode = "M130EJ";
        $request->acceptTermsAndConditions = true;
        $response = $this->client->Account->Create($request);
        $badPassword = 'password';
        $cprequest = new ChangePasswordRequest();
        $cprequest->emailaddress = $request->email;
        $cprequest->newpassword = $badPassword;
        $cprequest->currentpassword = $request->password;
        $response = $this->client->Account->ChangePassword($cprequest);

        $this->assertEquals($response->success, 1);
    }
    public function testGetAllDonations_WhenSuppliedAuthentication_ReturnListOfDonations() {
        $response = $this->client->Account->AllDonations();
        $this->assertNotNull($response);
    }
    public function testGetRatingHistory_WhenSuppliedAuthentication_ReturnListOfRatings() {
        $response = $this->client->Account->RatingHistory();
        $this->assertNotNull($response);
    }    
}
