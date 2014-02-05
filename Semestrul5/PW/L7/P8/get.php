<?php
mysql_connect("localhost", "root");
mysql_select_db("Local");
$result = mysql_query("select echipa, meciuri, victorii, egaluri, infrangeri, goluriMarcate, goluriPrimite, puncte
						   from ClasamentEchipe
						   order by puncte desc");
if ($result == FALSE)
	echo "error 1";
else
{
	if ($row = mysql_fetch_array($result))
	{
		echo $row[0];
		for ($rowIndex = 1; $rowIndex < 8; $rowIndex++)
			echo ",".$row[$rowIndex];
	}
	while ($row = mysql_fetch_array($result))
	{
		echo ";".$row[0];
		for ($rowIndex = 1; $rowIndex < 8; $rowIndex++)
			echo ",".$row[$rowIndex];
	}
}
mysql_close();
?>