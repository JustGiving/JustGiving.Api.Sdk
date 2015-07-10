<?php
include_once 'Base.php';
class CountriesTest extends Base {
    public function testGetCountries_ReturnCountries() {
        $response = $this->client->Countries->Countries();
        $this->assertNotNull($response);
    }
}
