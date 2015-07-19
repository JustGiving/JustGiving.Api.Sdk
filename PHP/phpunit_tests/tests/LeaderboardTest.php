<?php
include_once 'Base.php';
class LeaderboardTest extends Base {
    public function testGetCharityLeaderboard_WhenSuppliedWithValidCharityId_ReturnHttpStatusCode_200() {
        $response = $this->client->Leaderboard->GetCharityLeaderboard(2050);
        $this->assertEquals('200', $response->httpStatusCode);
    }

    public function testGetEventLeaderboard_WhenSuppliedWithValidEventId_ReturnHttpStatusCode_200() {
        $response = $this->client->Leaderboard->GetEventLeaderboard(479546);
        $this->assertEquals('200', $response->httpStatusCode);
    }      
}
