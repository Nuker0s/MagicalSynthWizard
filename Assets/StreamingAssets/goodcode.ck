[49, 54, 59, 64, 69] @=> int notes[];
me.arg(0).toInt()=> int key;
0.05=>float gains;
1 => int octave;
0.3::second=> dur beat;
SinOsc osc1 => LPF filter => ADSR env1 => NRev rev1 => dac;
TriOsc osc2 => ADSR env2 => NRev rev2 => dac;
440 => filter.freq;
0.3 => filter.Q;
gains => osc1.gain;
gains*0.1 => osc2.gain;

(beat/64,beat/8,0.6,10::ms) => env1.set;
(beat/64,beat/8,0.6,10::ms) => env2.set;
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
3 => key;
beat*3 => now;
press();
beat => now;
relese();
beat/16 => now;
1 => key;
press();
beat => now;
relese();
beat*10=> now;