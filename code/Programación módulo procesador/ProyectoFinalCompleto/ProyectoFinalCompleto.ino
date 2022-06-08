#include <EnableInterrupt.h>

#define BUSY 2  //Cambio por 2
#define RD 7  //RD+CS tied together // Cambio por 7
#define RESET 5
#define CONVST 6 //CONVSTA+CONVSTB soldered together on the board
#define RANGE 3
#define LV 9
                  //PÍN FÍSICO
#define DB0 14    //64
#define DB1 15    //63
#define DB2 16    //13
#define DB3 17    //12
#define DB4 18    //46
#define DB5 19    //45
#define DB6 20    //44
#define DB7 21    //43
#define DB8 22    //78
#define DB9 23    //77
#define DB10 24   //76
#define DB11 25   //75
#define DB12 26   //74
#define DB13 27   //73
#define DB14 28   //72
#define DB15 29   //71

// #define OS0 10 //23
// #define 0S1 11 //24
// #define 0S2 12 //25

// #define FRSTDATA 8 //17
// #define SHDN 42 //42

String imputString = "";
bool stringComplete = false;
int dfm = 224;

// declare rawData as an array of 16 integers
int rawData[16];

void setup() {

  enableInterrupt(BUSY, ISR_, FALLING);

  pinMode(RESET, OUTPUT);
  pinMode(CONVST, OUTPUT);
  pinMode(RD, OUTPUT);
  pinMode(RANGE, OUTPUT);
  pinMode(BUSY, INPUT);
  pinMode(LV, OUTPUT);
  pinMode(LED_BUILTIN, OUTPUT);

  Serial.begin(250000);
 // Serial.println("Ready"); // print "Ready" once

  //reset ADC to begin conversion 
  digitalWrite(RESET, HIGH);
  delayMicroseconds(10);
  digitalWrite(RESET, LOW);

  digitalWrite(CONVST, LOW);  
  digitalWrite(RD, HIGH);
  digitalWrite(RANGE, LOW);
  digitalWrite(BUSY, LOW);  
  digitalWrite(LV, LOW);
  digitalWrite(LED_BUILTIN, LOW);
  delayMicroseconds(1000);
}

void loop()
{
  if (stringComplete)
  {
    //imputString.trim();
    //Serial.println(imputString);
    //porcess the command
    if (imputString.equals("$R1$start"))
    {
      digitalWrite(RANGE, HIGH); //10 Voltios
      digitalWrite(LV, LOW);
      digitalWrite(LED_BUILTIN, HIGH);
      
      disableInterrupt(BUSY);
      delayMicroseconds(dfm); 
      digitalWrite(CONVST, LOW);
      delayMicroseconds(10); 
      digitalWrite(CONVST, HIGH);
      enableInterrupt(BUSY, ISR_, FALLING);
      
    } 
    else if (imputString.equals("$R2$start"))
    {
      digitalWrite(RANGE, LOW); //5 Voltios
      digitalWrite(LV, LOW);
      digitalWrite(LED_BUILTIN, HIGH);
      
      disableInterrupt(BUSY);
      delayMicroseconds(dfm); 
      digitalWrite(CONVST, LOW);
      delayMicroseconds(10); 
      digitalWrite(CONVST, HIGH);
      enableInterrupt(BUSY, ISR_, FALLING);
    } 
    else if (imputString.equals("$R3$start"))
    {
      digitalWrite(RANGE, LOW); //5 Voltios
      digitalWrite(LV, HIGH);
      digitalWrite(LED_BUILTIN, HIGH);
      
      disableInterrupt(BUSY);
      delayMicroseconds(dfm); 
      digitalWrite(CONVST, LOW);
      delayMicroseconds(10); 
      digitalWrite(CONVST, HIGH);
      enableInterrupt(BUSY, ISR_, FALLING);

      
    } 
    else if (imputString != "$R1$start" || imputString != "$R2$start" || imputString != "$R3$start")
    {
      //clear the string:
      digitalWrite(LED_BUILTIN, LOW);
      imputString = "";
      stringComplete = false;
      
      digitalWrite(RESET, HIGH);
      delayMicroseconds(10);
      digitalWrite(RESET, LOW);
    }
  }
}
void serialEvent()
{
  while (Serial.available())
  {
    //get the new byte:
    char inChar = (char)Serial.read();
    if (inChar == '\n')
    {
      stringComplete = true;
    }
    else
    {
      //add it to the imputString:
      //Serial.print(imputString);
      imputString += inChar;
      //Serial.print(imputString);
      
    }
  }
}

void ISR_()  {
  
  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH);
  Serial.print(",");

  delayMicroseconds(1);
  digitalWrite(RD, LOW);
  delayMicroseconds(1);
  readDBpins(); // do read
  digitalWrite(RD, HIGH); 

  Serial.println(" ");
}

void readDBpins()
{
  rawData[0] = digitalRead(DB15);
  if (rawData[0] > 0) {rawData[0] = 32768;} 
  rawData[1] = digitalRead(DB14);  
  if (rawData[1] > 0) {rawData[1] = 16384;}
  rawData[2] = digitalRead(DB13);
  if (rawData[2] > 0) {rawData[2] = 8192;}
  rawData[3] = digitalRead(DB12);
  if (rawData[3] > 0) {rawData[3] = 4096;}
  rawData[4] = digitalRead(DB11);
  if (rawData[4] > 0) {rawData[4] = 2048;}
  rawData[5] = digitalRead(DB10);
  if (rawData[5] > 0) {rawData[5] = 1024;}
  rawData[6] = digitalRead(DB9);
  if (rawData[6] > 0) {rawData[6] = 512;}
  rawData[7] = digitalRead(DB8);
  if (rawData[7] > 0) {rawData[7] = 256;}
  rawData[8] = digitalRead(DB7);
  if (rawData[8] > 0) {rawData[8] = 128;}
  rawData[9] = digitalRead(DB6);
  if (rawData[9] > 0) {rawData[9] = 64;}
  rawData[10] = digitalRead(DB5);
  if (rawData[10] > 0) {rawData[10] = 32;}
  rawData[11] = digitalRead(DB4);
  if (rawData[11] > 0) {rawData[11] = 16;}
  rawData[12] = digitalRead(DB3);
  if (rawData[12] > 0) {rawData[12] = 8;}
  rawData[13] = digitalRead(DB2);
  if (rawData[13] > 0) {rawData[13] = 4;}
  rawData[14] = digitalRead(DB1);
  if (rawData[14] > 0) {rawData[14] = 2;}
  rawData[15] = digitalRead(DB0);
  if (rawData[15] > 0) {rawData[15] = 1;}

  Serial.print (rawData[0] + rawData[1] + rawData[2] + rawData[3] +
                rawData[4] + rawData[5] + rawData[6] + rawData[7] + 
                rawData[8] + rawData[9] + rawData[10] + rawData[11] + 
                rawData[12] +rawData[13] + rawData[14] + rawData[15]);
}
