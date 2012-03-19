<?php 
 
    echo "pre_init<br/>";
  
    $ch = curl_init("http://www.google.com");
  
echo "post_init<br/>";

    curl_setopt($ch, CURLOPT_HEADER, false);
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);

echo "post_setopt<br/>";
echo "pre_exec<br/>";

    $result = curl_exec($ch);
    $code = curl_getinfo($ch, CURLINFO_HTTP_CODE);
    $type = curl_getinfo($ch, CURLINFO_CONTENT_TYPE);
    curl_close($ch);

echo "post_close<br/>";

echo "result: <br/>";

echo $result;
    //if (!strstr($type, 'application/json')) {
    //  throw new HttpResponseException('JSON response not found');
    //}

    //return new HttpResponse($code, $result);
