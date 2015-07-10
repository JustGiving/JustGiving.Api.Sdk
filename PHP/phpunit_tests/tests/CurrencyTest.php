<?php
include_once 'Base.php';
class CurrencyTest extends Base {
    public function testGetValidCurrencies_ReturnCurrencies() {
        $response = $this->client->Currency->ValidCurrencies();
        $this->assertNotNull($response);
    }
}
