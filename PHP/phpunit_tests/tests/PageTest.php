<?php
include_once 'Base.php';
class PageTest extends Base {
    public function testRetrieve_WhenSuppliedWithValidPage_ReturnsPageData() {
        $json = $this->client->Page->Retrieve("rasha25");
        $this->assertEquals($json->pageId, 640916);
        $this->assertEquals($json->activityId, 73347);
        $this->assertEquals($json->eventName, "rasha25");
        $this->assertEquals($json->pageShortName, "rasha25");
        $this->assertEquals($json->status, "Active");
        $this->assertEquals($json->owner, "Itgtm Wqepuy");
    }
    public function testListAll_WithValidCredentials_ReturnsListOfUserPages() {
        $pages = $this->client->Page->ListAll();
        $this->assertTrue(count($pages) > 0);
    }
    /**
    * test creating a page with a custom story.
    */
    public function testCreatePageWithStory() {
        $dto = new RegisterPageRequest();
        $dto->reference = "12345";
        $dto->pageShortName = "api-test-" . uniqid();
        $dto->activityType = "OtherCelebration";
        $dto->pageTitle = "api test";
        $dto->pageStory = "This is my custom page story, which will override the default.";
        $dto->eventName = "The Other Occasion of ApTest and APITest";
        $dto->charityId = 2050;
        $dto->targetAmount = 20;
        $dto->eventDate = "/Date(1235764800000)/";
        $dto->justGivingOptIn = true;
        $dto->charityOptIn = true;
        $dto->charityFunded = false;
        $page = $this->client->Page->Create($dto);

        $this->assertNotNull($page);
        $this->assertNotNull($page->next->uri);
        $this->assertNotNull($page->pageId);
        $json = $this->client->Page->Retrieve($dto->pageShortName);
        $this->assertEquals($json->story,'<p>This is my custom page story, which will override the default.</p>');
    }
    public function testCreate_ValidCredentials_CreatesNewPage() {
        $dto = new RegisterPageRequest();
        $dto->reference = "12345";
        $dto->pageShortName = "api-test-" . uniqid();
        $dto->activityType = "OtherCelebration";
        $dto->pageTitle = "api test";
        $dto->eventName = "The Other Occasion of ApTest and APITest";
        $dto->charityId = 2050;
        $dto->targetAmount = 20;
        $dto->eventDate = "/Date(1235764800000)/";
        $dto->justGivingOptIn = true;
        $dto->charityOptIn = true;
        $dto->charityFunded = false;
        $page = $this->client->Page->Create($dto);
        $this->assertNotNull($page);
        $this->assertNotNull($page->next->uri);
        $this->assertNotNull($page->pageId);
    }
    public function IsShortNameRegistered_KnownPage_Returns() {
        $pageShortName = "rasha25";
        $booleanResponse = $this->client->Page->IsShortNameRegistered($pageShortName);
        $this->assertTrue($booleanResponse);
        $pageShortName = uniqid();
        $booleanResponse = $client->Page->IsShortNameRegistered($pageShortName);
        $this->assertFalse($booleanResponse);
    }
    public function testUpdatePageStory_ForKnownPageWithValidCredentials_UpdatesStory() {
        $dto = new RegisterPageRequest();
        $dto->reference = "12345";
        $dto->pageShortName = "api-test-" . uniqid();
        $dto->activityType = "OtherCelebration";
        $dto->pageTitle = "api test";
        $dto->eventName = "The Other Occasion of ApTest and APITest";
        $dto->charityId = 2050;
        $dto->targetAmount = 20;
        $dto->eventDate = "/Date(1235764800000)/";
        $dto->justGivingOptIn = true;
        $dto->charityOptIn = true;
        $dto->charityFunded = false;
        $page = $this->client->Page->Create($dto);
        $update = "Updated this story with update - " . uniqid();
        $booleanResponse = $this->client->Page->UpdateStory($dto->pageShortName, $update);
        $this->assertTrue($booleanResponse);
    }
    public function testUploadImage_ForKnownPageWithValidCredentials_UploadsImageWithExpectedCaption() {
        $dto = new RegisterPageRequest();
        $dto->reference = "12345";
        $dto->pageShortName = "api-test-" . uniqid();
        $dto->activityType = "OtherCelebration";
        $dto->pageTitle = "api test";
        $dto->eventName = "The Other Occasion of ApTest and APITest";
        $dto->charityId = 2050;
        $dto->targetAmount = 20;
        $dto->eventDate = "/Date(1235764800000)/";
        $dto->justGivingOptIn = true;
        $dto->charityOptIn = true;
        $dto->charityFunded = false;
        $page = $this->client->Page->Create($dto);
        // Act
        $caption = "PHP Image Caption - " . uniqid();
        $filename = "jpg.jpg";
        $imageContentType = "image/jpeg";
        $booleanResponse = $this->client->Page->UploadImage($dto->pageShortName, $caption, $filename, $imageContentType);
        $this->assertTrue($booleanResponse);
    }
    public function testAddPostToPageUpdate_WhenSuppliedValidRequest_ReturnResponse() {
        $request = new AddPostToPageUpdateRequest();
        $request->Message = "update story";
        $response = $this->client->Page->AddPostToPageUpdate("api-test-54787f3435f75", $request);
        $this->assertNotNull($response->Created);
    }
}
