<?php
if (isset($_POST["echipa"])
	&& isset($_POST["meciuri"])
	&& isset($_POST["victorii"])
	&& isset($_POST["egaluri"])
	&& isset($_POST["infrangeri"])
	&& isset($_POST["goluriMarcate"])
	&& isset($_POST["goluriPrimite"])
	&& isset($_POST["puncte"]))
{
	$echipa = strip_tags(mysql_real_escape_string($_POST["echipa"]));
	$meciuri = strip_tags(mysql_real_escape_string($_POST["meciuri"]));
	$victorii = strip_tags(mysql_real_escape_string($_POST["victorii"]));
	$egaluri = strip_tags(mysql_real_escape_string($_POST["egaluri"]));
	$infrangeri = strip_tags(mysql_real_escape_string($_POST["infrangeri"]));
	$goluriMarcate = strip_tags(mysql_real_escape_string($_POST["goluriMarcate"]));
	$goluriPrimite = strip_tags(mysql_real_escape_string($_POST["goluriPrimite"]));
	$puncte = strip_tags(mysql_real_escape_string($_POST["puncte"]));
	mysql_connect("localhost", "root");
	mysql_select_db("Local");
	mysql_query("update ClasamentEchipe
					 set meciuri = $meciuri,
						 victorii = $victorii,
						 egaluri = $egaluri,
						 infrangeri = $infrangeri,
						 goluriMarcate = $goluriMarcate,
						 goluriPrimite = $goluriPrimite,
						 puncte = $puncte
					 where echipa = '$echipa'");
	mysql_close();
}
?>