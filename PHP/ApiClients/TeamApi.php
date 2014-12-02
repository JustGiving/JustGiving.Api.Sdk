<?php
include_once 'ClientBase.php';
include_once 'Http/CurlWrapper.php';

class TeamApi extends ClientBase
{		
	public $Parent;
	public $curlWrapper;
	
	public function __construct($justGivingApi)
	{
		$this->Parent		=	$justGivingApi;
		$this->curlWrapper	= new CurlWrapper();
	}

    public function Create($team)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/team/" + $team->teamShortName;
		$url = $this->BuildUrl($locationFormat);
		$payload = json_encode($team);
		$json = $this->curlWrapper->Put($url, $this->BuildAuthenticationValue(), $payload);
		return json_decode($json);
	}

	public function Team($teamShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/team/" . $teamShortName;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Get($url);
		return json_decode($json);
	}

	public function CheckIfExist($teamShortName)
	{
		$locationFormat = $this->Parent->RootDomain . "{apiKey}/v{apiVersion}/team/" . $teamShortName;
		$url = $this->BuildUrl($locationFormat);
		$json = $this->curlWrapper->Head($url);
		if($httpInfo['http_code'] == 200)
		{
			return true;
		}
		else if($httpInfo['http_code'] == 404)
		{
			return false;
		}
	}
}
