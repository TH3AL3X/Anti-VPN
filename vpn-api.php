<?php
$ip = $_GET['ip'];
                if ($_GET['ip'] == "") 
                { 
                    $resp = "Blacklisted";
                } 
                else  {
$resp = file_get_contents("http://check.getipintel.net/check.php?ip=".$ip."&contact=themachinehack@gmail.com&format=json&flags=m");

                echo $resp;
            } 
?>