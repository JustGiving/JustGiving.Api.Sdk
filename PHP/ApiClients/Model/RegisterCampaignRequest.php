<?php
class RegisterCampaignRequest
{
	public $campaignUrl;
	public $campaignName;
	public $campaignSummary;
	public $campaignStory;
	public $currencyCode;
	public $campaignTarget;
	public $campaignLogos;
	public $campaignCoverPhotos;
	public $campaignPhotos;
	public $campaignDeadline;
	public $campaignThankYouMessage;
	public $fundraisingEnabled;
} 

class CampaignCoverPhotos
{
	public $url;
	public $caption;
	public $title;
	public $alt;
}

class CampaignLogos
{
    public $url;
    public $caption;
    public $title;
    public $alt;
}

class CampaignPhotos
{
    public $url;
    public $caption;
    public $title;
    public $alt;
}
