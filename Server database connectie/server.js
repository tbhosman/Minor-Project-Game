var express = require('express');
var app = express();
var port = 8086;
var mysql = require('mysql');
var http = require('http');
var url = require('url');

var connection = mysql.createConnection({
  host     : 'localhost',
  user     : 'ewi3620tu5',
  password : 'GorcOgMeys3',
  database : 'ewi3620tu5'
});

var conn;

connection.connect(function(err) {
  if (err) {
    conn = "Not connected";
  }
  else{
	conn = "Connected!!!!!";
  }
});

app.use("/client", express.static('/client'));

app.get("/", function(req, res) {
	res.sendFile(__dirname + "/client/tabel.html");
	
});

app.get("/userid", function (req, res) {
	connection.query("SELECT * FROM Unity_data", function(err, rows, fields){
	var result = JSON.parse(JSON.stringify(rows));
	var size = Object.keys(result).length;
	var size1 = JSON.stringify(size);
		connection.query("INSERT INTO Unity_data (User_id) VALUES(" + size1 + ")", function(err, result) {
		if (err){
			res.send("error1");
		}
		res.send(JSON.stringify(size));
		});
		
	});
});

app.get("/overview", function (req, res) {
	connection.query("SELECT * FROM Unity_data", function(err, rows, fields){
		res.send( JSON.parse(JSON.stringify(rows)) );
	});
});

app.get("/average", function(req, res) {
	connection.query("SELECT avg(TotalGameTime), avg(PickUpTime_Key), avg(PickUpTime_Koevoet), avg(PickUpTime_SecurityCode), avg(PickUpTime_ReactorDoor), avg(PickUpTime_ScareNote), avg(OpenedDoor_Key), avg(OpenedDoor_Koevoet), avg(OpenedDoor_SecurityCode), avg(OpenedDoor_ReactorDoor), avg(OpenedDoor_ScareNote) FROM Unity_data WHERE TotalGameTime > 0", function(err, rows, fields){
		res.send(JSON.parse(JSON.stringify(rows)));
	});
});

app.get("/connection", function (req, res){
	res.send(conn);
});

app.get("/totalGameTime", function (req, res) {
	var total_game_time = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET TotalGameTime = " + total_game_time + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/pickUp1", function (req, res) {
	var time_pickup1 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET PickUpTime_Key = " + time_pickup1 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/pickUp2", function (req, res) {
	var time_pickup2 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET PickUpTime_Koevoet = " + time_pickup2 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/pickUp3", function (req, res) {
	var time_pickup3 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET PickUpTime_SecurityCode = " + time_pickup3 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/pickUp4", function (req, res) {
	var time_pickup4 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET PickUpTime_ReactorDoor = " + time_pickup4 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/pickUp5", function (req, res) {
	var time_pickup5 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET PickUpTime_ScareNote = " + time_pickup5 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/openedDoor1", function (req, res) {
	var time_door1 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET OpenedDoor_Key = " + time_door1 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/openedDoor2", function (req, res) {
	var time_door2 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET OpenedDoor_Koevoet = " + time_door2 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/openedDoor3", function (req, res) {
	var time_door3 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET OpenedDoor_SecurityCode = " + time_door3 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/openedDoor4", function (req, res) {
	var time_door4 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET OpenedDoor_ReactorDoor = " + time_door4 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/openedDoor5", function (req, res) {
	var time_door5 = req.param('Time');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET OpenedDoor_ScareNote = " + time_door5 + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.get("/gameOver", function (req, res) {
	var time_gameOver = req.param('Time');
	var location_x = req.param('Location_x');
	var location_y = req.param('Location_y');
	var location_z = req.param('Location_z');
	var user_id = req.param('User_id');
	connection.query("UPDATE Unity_data SET TotalGameTime = " + time_gameOver + ", GameOver_x = " + location_x + ", GameOver_y = " + location_y + ", GameOver_z = " + location_z + " WHERE User_id = " + user_id, function(err, result) {
	if (err){
		res.send("error2");
	}
	res.send(JSON.stringify(result));
	});
});

app.listen(port);

//key, security note, koevoet, ?controll room?
// 1 sleutel
// 2 koevoet
// 3 security code
// 5 scare note