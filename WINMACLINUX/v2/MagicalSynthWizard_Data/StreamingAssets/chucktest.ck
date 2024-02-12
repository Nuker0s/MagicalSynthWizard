[0, 0, 0, 0, 0] @=> global int keys[];
global float gains;

0 => global int octave;
[49, 54, 59, 64, 69] @=> int notes[];
me.arg(0).toInt()=> int key;
0.5::second=> dur beat;
SawOsc osc1 => LPF filter => ADSR env1 => NRev rev1 => dac;
SqrOsc osc2 => ADSR env2 => NRev rev2 => dac;

env1 => Delay delay1 => dac;
delay1 => delay1;
beat=> delay1.max;
beat/4 => delay1.delay;
0.2 => delay1.gain;

440 => filter.freq;
0.2 => filter.Q;
gains => osc1.gain;
gains*0.1 => osc2.gain;



(beat/32,beat/8,0.6,10::ms) => env1.set;
(beat/128,beat/32,0.1,1::ms) => env2.set;
0.03=> rev1.mix;
0.1 => rev2.mix;
//1::week => now;
fun void press()
{
    12*octave => int offset;
    Math.mtof(notes[key] + offset) => osc1.freq;
    Math.mtof(notes[key] + offset) => osc2.freq;
    //Math.mtof(notes[key] + offset) => filter.freq;
    1 => env1.keyOn;
    1 => env2.keyOn;
}
fun void relese()
{
    1 => env1.keyOff;
    1 => env2.keyOff;
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