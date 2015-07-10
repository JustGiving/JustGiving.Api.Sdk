<?php
include_once 'Base.php';
class CharityTest extends Base {
    public function testRetrieve_WhenSuppliedWithValidCharityId_RetrievesCharity() {
        $response = $this->client->Charity->Retrieve(2050);
        $this->assertEquals('The Demo Charity', $response->name);
    }
    public function testRetriveAccount_WhenSuppliedCorrectRequest_ReturnValid() {
        //need proper creds ?
        /*  $request = new AuthenticateCharityAccountRequest();
                $request->password = "badPassword";
                $request->pin = "1111";
                $request->username = "apiunittest_charity@justgiving.com";
                $response = $this->client->Charity->Authenticate($request);
                $this->assertEquals($response->isValid ,1);
                $this->assertTrue($response->charityId > 0);
                $this->assertNull($response->error)
        */
    }
    public function testGetEventsByCharityId_WhenSuppliedCorrectCharityId_ReturnEvents() {
        $response = $this->client->Charity->GetEventsByCharityId(2050);
        $this->assertNotNull($response);
        $this->assertNotNull($response->events);
    }
    public function testGetDonations_WhenSuppliedCorrectCharityId_ReturnDonations() {
        $response = $this->client->Charity->GetDonations(2050);
        $this->assertNotNull($response);
        $this->assertNotNull($response->donations);
    }
    function testGetCategories_ReturnCategories() {
        $response = $this->client->Charity->Categories();
        $this->assertNotNull($response);
    }
}
