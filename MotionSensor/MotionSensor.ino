/*
This a simple example of the aREST Library for Arduino (Uno/Mega/Due/Teensy)
using the Ethernet library (for example to be used with the Ethernet shield).
See the README file for more details.

Written in 2014 by Marco Schwartz under a GPL license.
*/

// Libraries
#include <SPI.h>
#include <Ethernet.h>
#include <aREST.h>
#include <avr/wdt.h>

#define PIRPIN 2     // what digital pin we're connected to

// Enter a MAC address for your controller below.
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x2B, 0x3C, 0x72 }; // Motion Sensor Wohnzimmer
byte mac[] = { 0x90, 0xA2, 0xDA, 0x0B, 0xBD, 0x71 }; // Motion Sensor Bad
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x53, 0x9B, 0xF7 }; // Motion Sensor WC
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x3B, 0x83, 0x17 }; // Motion Sensor Ankleide
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x59, 0x64, 0xE7 }; // Motion Sensor Schlafzimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x8C, 0x56, 0xF0 }; // Motion Sensor Gästezimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x1D, 0x3F, 0xBA }; // Motion Sensor Kinderzimmer
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x1D, 0x3F, 0xBA }; // Motion Sensor Flur1
//byte mac[] = { 0x90, 0xA2, 0xDA, 0x1D, 0x3F, 0xBA }; // Motion Sensor Flur2

// IP address in case DHCP fails
//IPAddress ip(192, 168, 0, 227); // Motion Sensor Wohnzimmer
IPAddress ip(192, 168, 0, 228); // Motion Sensor Bad
//IPAddress ip(192, 168, 0, 229); // Motion Sensor WC
//IPAddress ip(192, 168, 0, 230); // Motion Sensor Ankleide
//IPAddress ip(192, 168, 0, 231); // Motion Sensor Schlafzimmer
//IPAddress ip(192, 168, 0, 232); // Motion Sensor Gästezimmer
//IPAddress ip(192, 168, 0, 233); // Motion Sensor Kinderzimmer
//IPAddress ip(192, 168, 0, 234); // Motion Sensor Flur1
//IPAddress ip(192, 168, 0, 235); // Motion Sensor Flur2

// Ethernet server
EthernetServer server(80);

// Create aREST instance
aREST rest = aREST();

// Variables to be exposed to the API
int motionDetected;

void setup(void)
{
	// Start Serial
	Serial.begin(115200);

	pinMode(PIRPIN, INPUT);

	// Function to be exposed
	rest.function("detectMotion", detectMotion);

	// Give name & ID to the device (ID should be 6 characters long)
	rest.set_id("008");
	rest.set_name("dapper_drake");

	Ethernet.begin(mac, ip);

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
int detectMotion(String command) {

	// Get state from command
	motionDetected = digitalRead(PIRPIN);

	return motionDetected;
}