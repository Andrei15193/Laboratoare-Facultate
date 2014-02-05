<?php
session_start();
if (isset($_GET["page"]))
	$page = strip_tags(mysql_real_escape_string($_GET["page"]));
else
	$page = "Home";
if (isset($_SESSION["username"]))
{
	mysql_connect("localhost", "root");
	mysql_select_db("Local");
	$contentQueryResult = mysql_query("delete from Pages where title = '$page'");
	mysql_close();
}
header("Location: index.php");
?>