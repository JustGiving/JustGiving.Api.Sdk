<?php
include_once 'Base.php';
class CharityTest extends Base {
    public function testRetrieve_WhenSuppliedWithValidCharityId_RetrievesCharity() {
        $response = $this->client->Charity->Retrieve(2050);
        $this->assertEquals('The Demo Charity1', $response->name);
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
    public function testGetCategories_ReturnCategories() {
        $response = $this->client->Charity->Categories();
        $this->assertNotNull($response);
    }
}
