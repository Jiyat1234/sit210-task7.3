import RPi.GPIO as GPIO
import time
//ultrasonic sensor
trig_pin = 23
echo_pin = 26

GPIO.setmode(GPIO.BCM)
GPIO.setup(17, GPIO.OUT)
GPIO.setup(trig_pin,GPIO.OUT)
GPIO.setup(echo_pin,GPIO.OUT)


led = GPIO.PWM(17,500)//create a led that will initializes PWM with the GPIO pin 17 and Frequency of 500hz

// start the PWM with 0
led.start(0)

def object_distance(): //func to get distance of the object from the sensor
	
    GPIO.output(trig_pin, True) // pulse sent to sensor for getting the sensing the distance
   
    time.sleep(0.00001)
    GPIO.output(trig_pin, False)

    
    pulse_start = time.time() // time recorded at which the sensor sensed the object
    pulse_end = time.time()
    

    while GPIO.input(echo_pin) == 0:  // time taken by the sensor to get the pulse back
        pulse_start = time.time()
   
    while GPIO.input(echo_pin) == 1:
        pulse_end = time.time()

    pulse_duration = pulse_end - pulse_start // the time of the pulse calculated in reference to the disnatnce
    distance = pulse_duration * 17150  // calculating dist in cm - Speed of sound in cm/s

    return distance

while (1):
	distance = getting_distnace()
	
	brightness = max(0, min(100, int(100 - (distance / 10)))) // led intensity changes according to the movement
	
	led.ChangeDutyCycle(brightness)
	time.sleep(0.1)
