<?php

class Helpers
{
	/*Input should be in '/Date(1365004652303-0500)/' format
	Taken from http://stackoverflow.com/questions/16749778/php-date-format-date1365004652303-0500/16750148#16750148 */
	public function TransformEpochToDateTime($date){		
		preg_match('/(\d{10})(\d{3})([\+\-]\d{4})/', $date, $matches);
		$dt = DateTime::createFromFormat("U.u.O",vsprintf('%2$s.%3$s.%4$s', $matches));
		echo $dt->format('r');
	}
}