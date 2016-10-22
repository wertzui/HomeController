/*
 Name:		TemperatureSensor.ino
 Created:	4/17/2016 2:03:24 PM
 Author:	spbwe
*/

#include <Ethernet.h>
#include <SPI.h>
#include <DHT.h>
#include "RestClient.h"

// rest
RestClient client = RestClient("192.168.0.198", 1907);
String response;
void test_response() {
	if (response == "OK") {
		Serial.println("TEST RESULT: ok (response body)");
	}
	else {
		Serial.println("TEST RESULT: fail (response body = " + response + ")");
	}
	response = "";
}

// temperature
#define DHTPIN 2     // what digital pin we're connected to
// Uncomment whatever type you're using!
//#define DHTTYPE DHT11   // DHT 11
#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321
//#define DHTTYPE DHT21   // DHT 21 (AM2301)
DHT dht(DHTPIN, DHTTYPE);
float temperature;
float humidity;
bool anyChanges;
char messageFormat[] = "{name:\"MeasuredTemperatureWohnzimmer\",temperature:%02d.%02d,humidity:%02d.%02d}";
char message[] = "{name:\"MeasuredTemperatureWohnzimmer\",temperature:00.00,humidity:00.00}";

// the setup function runs once when you press reset or power the board
void setup() {
	Serial.begin(115200);
	Serial.println("Starting client");
	client.dhcp();
	client.setContentType("application/json");
	Serial.println("Client started");
	dht.begin();
}

// the loop function runs over and over again until power down or reset
void loop() {
	delay(2000);
	readTemperature();
	sendTemperature();
}

void readTemperature()
{
	float t = dht.readTemperature();
	float h = dht.readHumidity();

	if (temperature != t)
	{
		temperature = t;
		anyChanges = true;
	}

	if (humidity != h)
	{
		humidity = h;
		anyChanges = true;
	}
}

void sendTemperature()
{
	if (anyChanges)
	{
		anyChanges = false;

		constructMessage();

		Serial.println(message);
		client.post("/Temperature", message, &response);
		test_response();
	}
}

void constructMessage()
{
	// sprintf does only work with integers on the arduino, so we have to format the decimal places as separate integers
	sprintf(message, messageFormat,
		(int)temperature, (int)((temperature - ((int)temperature)) * 100),
		(int)humidity, (int)((humidity - ((int)humidity)) * 100));
}
