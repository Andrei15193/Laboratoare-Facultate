<?php
session_start();
mysql_connect("localhost", "root");
mysql_select_db("Local");
$skip = FALSE;
$alreadyExists = FALSE;
if (isset($_POST["title"]) && isset($_POST["content"]))
{
	$pageTitle = strip_tags(mysql_real_escape_string($_POST["title"]));
	if (strlen($pageTitle) == 0)
		$pageTitle = "(no title)";
	$pageContent = mysql_real_escape_string($_POST["content"]);
	if (mysql_result(mysql_query("select count(*) from Pages where title = '$pageTitle'"), 0) > 0)
		$alreadyExists = TRUE;
	else
	{
		mysql_query("insert into Pages(title, content) values ('$pageTitle', '$pageContent')");
		header("Location: index.php");
		$skip = TRUE;
	}
}
mysql_close();
?>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Laborator 6 - Add new page</title>
	<link rel="stylesheet" type="text/css" href="default.css" />
</head>
<body>
	<div id="header">
		<h1>Programare Web Laborator 6</h1>
	</div>
	<?php
	if (!$skip)
	{
		echo "<div id=\"menuBar\"><a href=\"index.php\" class=\"small\">Return to index page</a>";
		if (isset($_SESSION["username"]))
		{
			echo " <a href=\"logout.php\" class=\"small\">Logout ".$_SESSION["username"]."</a></div>";
			echo "<div id=\"content\" style = \"margin-left: 10px;\">";
			echo "<h1>Add new page</h1>";
			if ($alreadyExists)
			{
				echo "<div class=\"error\"><p>The page already exists!</p></div>";
				echo "<form method=\"post\" action=\"newPage.php\">";
				echo "<table><tr>";
				echo "<td><label>Title:</label></td>";
				echo "<td><input name=\"title\" style=\"width: 100%;\" type=\"text\" value=\"".$_POST["title"]."\" /></td>";
				echo "</tr><tr>";
				echo "<td><label>Content:</label></td>";
				echo "<td style=\"width: 100%;\"><textarea style=\"width: 100%; height: 300px;\" name=\"content\">".$_POST["content"]."</textarea></td>";
				echo "</tr>";
			}
			else
			{
				echo "<form method=\"post\" action=\"newPage.php\">";
				echo "<table><tr>";
				echo "<td><label>Title:</label></td>";
				echo "<td><input name=\"title\" style=\"width: 100%;\" type=\"text\" /></td>";
				echo "</tr><tr>";
				echo "<td><label>Content:</label></td>";
				echo "<td style=\"width: 100%;\"><textarea style=\"width: 100%; height: 300px;\" name=\"content\"></textarea></td>";
				echo "</tr>";
			}
			echo "<tr><td></td><td><input class=\"button\" type=\"submit\" value=\"Add\" /></td>";
			echo "</table></form>";
		}
		else
		{
			echo "</div><div id=\"content\" style=\"margin-left: 10px;\">";
			echo "<h1>How cool would that be?</h1>";
			echo "<p>You do not have permission to access this age, please log in and try again.</p>";
			echo "</div>";
		}
	}
	?>
</body>
</html>
