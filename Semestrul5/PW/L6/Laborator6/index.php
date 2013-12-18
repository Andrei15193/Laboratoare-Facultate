<?php
session_start();
if (isset($_GET["page"]))
	$page = strip_tags(mysql_real_escape_string($_GET["page"]));
else
	$page = "Home";
mysql_connect("localhost", "root");
mysql_select_db("Local");
$contentQueryResult = mysql_query("select title, content from Pages where title='$page'");
$pagesQueryResult = mysql_query("select title from Pages");
?>
<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="utf-8" />
	<?php
	if ($contentQueryResult == FALSE || mysql_num_rows($contentQueryResult) == 0)
	{
		$contentPage = FALSE;
		echo "<title>Laborator 6 - 404</title>";
	}
	else
	{
		$contentPage = mysql_fetch_array($contentQueryResult);
		echo "<title>Laborator 6 - ".$contentPage[0]."</title>";
	}
	?>
	<link rel="stylesheet" type="text/css" href="default.css" />
</head>
<body>
	<div id="header">
		<h1>Programare Web Laborator 6</h1>
	</div>
	<?php
	echo "<div id=\"menuBar\">";
	if (isset($_SESSION["username"]))
		echo "<a href=\"logout.php\" class=\"small\">Logout ".$_SESSION["username"]."</a>";
	else
		echo "<a href=\"login.php\" class=\"small\">Administrator view</a>";
	echo "</div>";
	
	if ($pagesQueryResult == FALSE)
		echo "<div class=\"error\"><h1>Connection error!</h1><p>We are sorry for this inconvenience however we could not make a connection to the database server, please try again later...</p></div>";
	else
		if (isset($_SESSION["username"]) || mysql_num_rows($pagesQueryResult) > 0)
		{
			echo "<ul id=\"menu\">";
			while ($row = mysql_fetch_array($pagesQueryResult))
			{
				echo "<li><a class=\"button\" href=\"?page=".$row[0]."\">".$row[0]."</a>";
				if (isset($_SESSION["username"]))
				{
					echo "<div class=\"menuOptionBar\">";
					echo "<a href=\"editPage.php?page=".$row[0]."\"><img src=\"images/edit.png\" alt=\"Edit\" /></a>";
					echo "<a href=\"removePage.php?page=".$row[0]."\"><img src=\"images/delete.png\" alt=\"Delete\" /></a>";
					echo "</div>";
				}
				echo "</li>";
			}
			if (isset($_SESSION["username"]))
				echo "<li><a class=\"button\" href=\"newPage.php\">Add page</a></li>";
			echo "</ul>";
			echo "<div id=\"content\">";
			if ($contentPage == FALSE)
				echo "<div class=\"error\"><h1>404 Page not found!</h1><p>The page you are looking for does not exist!</p></div>";
			else
				echo $contentPage[1];
			echo "</div>";
		}
	?>
</body>
</html>
<?php
mysql_close();
?>