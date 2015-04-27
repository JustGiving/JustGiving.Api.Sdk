<?php

class TeamMember {
	public $pageShortName;	           
}

class Team
{
	public $teamShortName;
    public $name;
	public $story;
	public $targetType;
	public $teamType;
	public $target;
	public $teamMembers;

	public function __construct(){
		$this->teamMembers = array();
	}
}

