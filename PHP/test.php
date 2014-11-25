<?php
include_once 'JustGivingClient.php';
include_once '/ApiClients/Model/ValidateAccountRequest.php';
include_once '/ApiClients/Model/RateContentRequest.php';
include_once '/ApiClients/Model/AddInterestRequest.php';
include_once '/ApiClients/Model/AuthenticateCharityAccountRequest.php';

ini_set('display_errors', 'On');
error_reporting(E_ALL | E_STRICT);


//$client = new JustGivingClient("https://api-sandbox.justgiving.com/", "0f938d22", 1, "info@helbards.com", "sobieskie1");
$client = new JustGivingClient("https://api-sandbox.justgiving.com/", "0f938d22", 1, "apiunittest@justgiving.com", "password");
$request = new AuthenticateCharityAccountRequest();
$request->password = "badPassword";
$request->pin = "1111";
$request->username = "apiunittest_charity@justgiving.com";

$response = $client->Currency->ValidCurrencies();
echo $response[0]->currencyCode;

?>