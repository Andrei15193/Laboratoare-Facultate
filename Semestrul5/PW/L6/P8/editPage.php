<?php
session_start();
if (isset($_GET["page"]))
	$page = strip_tags(mysql_real_escape_string($_GET["page"]));
else
	$page = "Home";
mysql_connect("localhost", "root");
mysql_select_db("Local");
$contentQueryResult = mysql_query("select title, content from Pages where title='$page'");
$skip = FALSE;
$alreadyExists = FALSE;
if (isset($_POST["title"]) && isset($_POST["content"]))
{
	$pageTitle = strip_tags(mysql_real_escape_string($_POST["title"]));
	if (strlen($pageTitle) == 0)
		$pageTitle = "(no title)";
	if ($page != $pageTitle && mysql_result(mysql_query("select count(*) from Pages where title = '$pageTitle'"), 0) > 0)
		$alreadyExists = TRUE;
	else
	{
		$pageContent = mysql_real_escape_string($_POST["content"]);
		mysql_query("update Pages set title = '$pageTitle', content = '$pageContent' where title = '$page'");
		header("Location: index.php?page=$pageTitle");
		$skip = TRUE;
	}
}
mysql_close();
?>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<title>Laborator 6 - Edit page</title>
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
			if (mysql_num_rows($contentQueryResult) == 0)
			{
				echo "<div class=\"error\"><h1>Missing something?</h1>";
				echo "<p>The page $page does not exist!</p></div>";
			}
			else
			{
				if ($alreadyExists)
				{
					echo "<h1>Edit page: $page</h1>";
					echo "<div class=\"error\"><p>The page ".$_POST["title"]." already exists! Please use a different page title.</p></div>";
					echo "<form method=\"post\" action=\"editPage.php?page=$page\">";
					echo "<table><tr>";
					echo "<td><label>Title:</label></td>";
					echo "<td><input style=\"width: 100%;\" name=\"title\" type=\"text\" value=\"".$_POST["title"]."\" /></td>";
					echo "</tr><tr>";
					echo "<td><label>Content:</label></td>";
					echo "<td style=\"width: 100%;\"><textarea style=\"width: 100%; height: 300px;\" name=\"content\">".$_POST["content"]."</textarea></td>";
					echo "</tr>";
				}
				else
				{
					$content = mysql_fetch_array($contentQueryResult);
					echo "<h1>Edit page: $page</h1>";
					echo "<form method=\"post\" action=\"editPage.php?page=$page\">";
					echo "<table><tr>";
					echo "<td><label>Title:</label></td>";
					echo "<td><input style=\"width: 100%;\" name=\"title\" type=\"text\" value=\"".$content[0]."\" /></td>";
					echo "</tr><tr>";
					echo "<td><label>Content:</label></td>";
					echo "<td style=\"width: 100%;\"><textarea style=\"width: 100%; height: 300px;\" name=\"content\">".$content[1]."</textarea></td>";
					echo "</tr>";
				}
				echo "<tr><td></td><td><input class=\"button\" type=\"submit\" value=\"Edit\" /></td>";
				echo "</table></form>";
			}
			echo "</div>";
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
