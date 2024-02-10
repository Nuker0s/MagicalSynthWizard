[49, 54, 59, 64, 69] @=> int notes[];
me.arg(0).toInt()=> int key;
0.05=>float gains;
0 => int octave;
0.5::second=> dur beat;
SinOsc osc1 => LPF filter => ADSR env1 => NRev rev1 => dac;
SawOsc osc2 => ADSR env2 => NRev rev2 => dac;
440 => filter.freq;
0.4 => filter.Q;
gains => osc1.gain;
gains*0.1 => osc2.gain;

(beat/32,beat/8,0.6,10::ms) => env1.set;
(beat/128,beat/32,0.1,1::ms) => env2.set;
0.03=> rev1.mix;
0.1 => rev2.mix;

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
press();
beat => now;
relese();
beat => now;