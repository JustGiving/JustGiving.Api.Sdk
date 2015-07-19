<?php
include_once 'Base.php';
class SearchTest extends Base {
    public function testCharitySearch_KeywordWithKnownResults_SearchResultsPresent() {
        $response = $this->client->Search->CharitySearch('the demo charity1');
        foreach ($response->charitySearchResults as $charity) {
            if ($charity->charityId == 2050) {
                $this->assertEquals(strtolower($charity->name), 'the demo charity (justgiving demo)');
            }
        }
    }
}
