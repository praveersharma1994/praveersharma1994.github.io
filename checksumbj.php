<?php 
if (!empty($_POST))
{
 $secretKey = "396eac0e84e0397432c1885afa61ea3f083ae9f1";
 
foreach($_POST as $key => $value) {
    $postData[$key] = $_POST[$key];
 }
 // get secret key from your config
 ksort($postData);
 $checksumData = "";
 foreach ($postData as $key => $value){
      $checksumData .= $key.$value;
 }

 $checksum = hash_hmac('sha256', $checksumData, $secretKey,true);
 $checksum = base64_encode($checksum);
 
 // This is how the response is expected

 $response = array("status" => "OK", "checksum" => $checksum);

 echo json_encode($response);
}
else // $_POST is empty.
{
    echo "Perform code for page without POST data. ";
}

?>