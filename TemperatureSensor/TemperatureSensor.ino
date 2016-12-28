/*
This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
using the Ethernet library (for example to be used with the Ethernet shield).
See the README file for more details.

Written in 2014 by Marco Schwartz under a GPL license.
*/

// Libraries
#include <DHT_U.h>
#include <DHT.h>
#include <SPI.h>
#include <Ethernet.h>
#include <aREST.h>
#include <avr/wdt.h>

#define DHTPIN 2     // what digital pin we're connected to
#define DHTTYPE DHT22   // DHT 22  (AM2302), AM2321
DHT dht(DHTPIN, DHTTYPE);

// Enter a MAC address for your controller below.
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x0E, 0xFE, 0x40 }; // Heating Actor
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x48, 0x8B, 0xF8 }; // Temperature Sensor Wohnzimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0xD2, 0x39, 0x1D }; // Temperature Sensor Bad
//byte mac[] = { 0x90, 0xA2, 0xDA, 0xDB, 0xD0, 0x4B }; // Temperature Sensor WC
//byte mac[] = { 0x90, 0xA2, 0xDA, 0xA4, 0xBB, 0x6E }; // Temperature Sensor Ankleide
//byte mac[] = { 0x90, 0xA2, 0xDA, 0xB1, 0x4A, 0x75 }; // Temperature Sensor Schlafzimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x01, 0x02, 0xC6 }; // Temperature Sensor Gästezimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x5F, 0x86, 0xB3 }; // Temperature Sensor Kinderzimmer

// IP address in case DHCP fails
//IPAddress ip(192, 168, 0, 55); // Heating Actor
//IPAddress ip(192, 168, 0, 220); // Temperature Sensor Wohnzimmer
//IPAddress ip(192, 168, 0, 221); // Temperature Sensor Bad
//IPAddress ip(192, 168, 0, 222); // Temperature Sensor WC
//IPAddress ip(192, 168, 0, 223); // Temperature Sensor Ankleide
//IPAddress ip(192, 168, 0, 224); // Temperature Sensor Schlafzimmer
//IPAddress ip(192, 168, 0, 225); // Temperature Sensor Gästezimmer
//IPAddress ip(192, 168, 0, 226); // Temperature Sensor Kinderzimmer

// Ethernet server
EthernetServer server(80);

// Create aREST instance
aREST rest = aREST();

// Variables to be exposed to the API
int temperatureTimes10;
int humidityTimes10;
float temperature;
float humidity;
int updateResult[] = { temperatureTimes10 , humidityTimes10 };

void setup(void)
{
	// Start Serial
	Serial.begin(115200);

	dht.begin();

	// Init variables and expose them to REST API
	temperatureTimes10 = 0;
	humidityTimes10 = 0;
	rest.variable("temperature", &temperatureTimes10);
	rest.variable("humidity", &humidityTimes10);

	// Function to be exposed
	rest.function("update", update);

	// Give name & ID to the device (ID should be 6 characters long)
	rest.set_id("008");
	rest.set_name("dapper_drake");

	// Start the Ethernet connection and the server
	//if (Ethernet.begin(mac) == 0) {
		//Serial.println("Failed to configure Ethernet using DHCP");
		// no point in carrying on, so do nothing forevermore:
		// try to congifure using IP address instead of DHCP:
		Ethernet.begin(mac, ip);
	//}
	server.begin();
	Serial.print("server is at ");
	Serial.println(Ethernet.localIP());

	// Start watchdog
	wdt_enable(WDTO_4S);
}

void loop() {

	// listen for incoming clients
	EthernetClient client = server.available();
	rest.handle(client);
	//rest.handle(Serial);
	wdt_reset();

}

// Custom function accessible by the API
int update(String command) {

	// Get state from command
	humidity = dht.readHumidity();
	temperature = dht.readTemperature();

	humidityTimes10 = humidity * 10;
	temperatureTimes10 = temperature * 10;

	updateResult[0] = temperatureTimes10;
	updateResult[1] = humidityTimes10;

	return 0;
}