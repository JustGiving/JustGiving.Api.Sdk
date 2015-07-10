<?php
include_once 'Base.php';
class EventTest extends Base {
    public function testRetrieveEvent_IssuedWithKnownId_ReturnsEvent() {
        $response = $this->client->Event->Retrieve(479546);
        $this->assertEquals($response->name, 'Virgin London Marathon 2011 - Applying for a charity place');
    }
    public function testRetrievePages_IssuedWithKnownId_ReturnsPages() {
        $response = $this->client->Event->RetrievePages(479546);
        $this->assertTrue(count($response->fundraisingPages) > 0);
    }
}
