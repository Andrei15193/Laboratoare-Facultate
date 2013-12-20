<?php
session_start();
$wrongCredentials = FALSE;
mysql_connect("localhost", "root");
mysql_select_db("Local");
if (isset($_POST["username"]) && isset($_POST["password"]))
{
	$username = mysql_real_escape_string($_POST["username"]);
	$password = sha1($_POST["password"]);
	$result = mysql_query("select name from Users where name='$username' and passwordHash = '$password'");
	if ($result && 1 == mysql_num_rows($result))
	{
		$_SESSION["username"] = mysql_result($result, 0);
		header("Location: index.php");
	}
	else
		$wrongCredentials = TRUE;
}
$pagesQueryResult = mysql_query("select title from Pages");
?>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Laborator 6 - Index</title>
	<link rel="stylesheet" type="text/css" href="default.css" />
</head>
<body>
	<div id="header">
		<h1>Programare Web Laborator 6</h1>
	</div>
	<div id="menuBar">
		<a href="index.php" class="small">Return to previous page</a>
	</div>
	<?php
	if ($wrongCredentials)
		echo "<div class=\"error\"><h1>Invalid credentials!</h1><p>The username/password you have provided are not valid.</p></div>";
	if (mysql_num_rows($pagesQueryResult) > 0)
	{
		echo "<ul id=\"menu\">";
		while ($row = mysql_fetch_array($pagesQueryResult))
		{
			echo "<li><a class=\"button\" href=\"index.php?page=".$row[0]."\">".$row[0]."</a>";
			if (isset($_SESSION["username"]))
			{
				echo "<div class=\"menuOptionBar\">";
				echo "<a href=\"editPage.php?page=".$row[0]."\"><img src=\"images/edit.png\" alt=\"Edit\" /></a>";
				echo "<a href=\"removePage.php?page=".$row[0]."\"><img src=\"images/delete.png\" alt=\"Delete\" /></a>";
				echo "</div>";
			}
			echo "</li>";
		}
		echo "</ul>";
	}
	if ($wrongCredentials)
	{
		echo "<div id=\"content\"><form action=\"login.php\" method=\"post\">";
		echo "<table><tr><td><label>Username: </label></td>";
		echo "<td><input type=\"text\" name=\"username\" value=\"".$_POST["username"]."\"/></td></tr>";
	}
	else
	{
		echo "<div id=\"content\"><form action=\"login.php\" method=\"post\">";
		echo "<table><tr><td><label>Username: </label></td>";
		echo "<td><input type=\"text\" name=\"username\" /></td></tr>";
	}
	echo "<tr><td><label>Password: </label></td>";
	echo "<td><input type=\"password\" name=\"password\" /></td></tr>";
	echo "<tr><td></td><td><input class=\"button\" type=\"submit\" value=\"Login\" /></td></tr>";
	echo "</table></form></div>";
	?>
</body>
</html>
<?php
mysql_close();
?>