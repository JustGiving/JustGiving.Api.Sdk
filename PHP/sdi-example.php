<html>
<head>
  <title>SDI Example</title>
</head>
<body>

<?php

if (isset($_GET['donationId'])) {
  $donationId = $_GET['donationId'];
}

if (!isset($donationId)) { ?>

<a href="http://v3-sandbox.justgiving.com/donation/direct/charity/2050?frequency=single&exitUrl=http%3A%2F%2Flocalhost:678/sdi-example.php?donationId=JUSTGIVING-DONATION-ID">Click here to start your SDI donation to The Demo Charity on Sandbox</a>

<?php } 

if (isset($donationId)) { ?>

<p>Great, you've made your donation, with Id: <?php echo $donationId ?></p>

<?php

include_once 'JustGivingClient.php';
	
$client = new JustGivingClient("https://api-sandbox.justgiving.com/", "c1072ac8", 1);

$response = $client->Donation->Retrieve($donationId);
  
echo "Donation amount: " . $response->amount . "<br/>";
echo "Donation date: " . $response->donationDate . "<br/>";
echo "Donation Donor: " . $response->donorDisplayName . "<br/>";
echo "Status: " . $response->status . "<br/>";
  
?>


<?php } ?>

</body>
</html>

