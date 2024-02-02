[0, 0, 0, 0, 0] @=> global int keys[];
global float gains;

[44, 47, 50, 52, 55] @=> int notes[];
me.arg(0).toInt()=> int key;
0.5::second=> dur beat;
TriOsc osc1 => ADSR env1 => NRev rev1 => dac;
10 => int offset;
gains => osc1.gain;

Math.mtof(notes[key] + offset) => osc1.freq;

(beat/32,beat/8,0.3,10::ms) => env1.set;
0.03=> rev1.mix;
//1::week => now;
fun void press()
{
    1 => env1.keyOn;
}
fun void relese()
{
    1 => env1.keyOff;
}
while (true)
{
    //<<<keys[0],keys[1],keys[2]>>>;
    if(keys[key]==1)
    {
        press();
        
    }
    else relese();
    1::ms => now;
}

// Set up five frequency values for each key (corresponding to piano keys C, D, E, F, G)
