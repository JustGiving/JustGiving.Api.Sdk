<?php
include_once 'Base.php';
class DonationTest extends Base {
    public function testRetrieve_WhenSuppliedWithKnownExistingDonationId_ReturnsDonation() {
        $response = $this->client->Donation->Retrieve(21303723);
        $this->assertNotNull($response->amount);
        $this->assertEquals($response->currencyCode, "GBP");
        $this->assertEquals($response->status, "Accepted");
    }
}
