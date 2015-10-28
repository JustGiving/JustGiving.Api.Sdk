<?php
include_once "../JustGivingClient.php";
include_once "../ApiClients/Model/AddPostToPageUpdateRequest.php";
include_once '../ApiClients/Model/CreateAccountRequest.php';
include_once '../ApiClients/Model/ValidateAccountRequest.php';
include_once '../ApiClients/Model/ChangePasswordRequest.php';
include_once '../ApiClients/Model/RateContentRequest.php';
include_once '../ApiClients/Model/AddInterestRequest.php';

class Base extends \PHPUnit_Framework_TestCase {
    protected function setUp() {
        $testContext = new TestContext();
        $this->context = $testContext;
        $this->client = new JustGivingClient($testContext->ApiLocation, $testContext->ApiKey, $testContext->ApiVersion, $testContext->TestUsername, $testContext->TestValidPassword);
        $this->client->debug = $testContext->Debug;
    }
    public function testTrueIsTrue() {
        $foo = true;
        $this->assertTrue($foo);
    }
}
class TestContext {
    public $ApiLocation;
    public $ApiKey;
    public $TestUsername;
    public $TestValidPassword;
    public $TestInvalidPassword;
    public $ApiVersion;
    public $Debug;
    public function __construct() {
        $this->ApiLocation = "https://api.sandbox.justgiving.com/";
        $this->ApiKey = "decbf1d2";
        $this->TestUsername = "apiunittest@justgiving.com";
        $this->TestValidPassword = "password";
        $this->TestInvalidPassword = "badPassword";
        $this->ApiVersion = 1;
        $this->Debug = true;
    }
}
