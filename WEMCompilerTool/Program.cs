﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEMCompiler.FFmpegHook;
using WEMCompiler.WWWem;

namespace WEMCompilerTool {
	class Program {
		static void Main(string[] args) {

			if (args.Length == 0) {
				Console.WriteLine("Drag n' drop a WEM or any audio file onto this EXE to convert it. WEM will be converted to WAV no matter what.");
				Console.WriteLine("Press any key to quit...");
				Console.ReadKey(true);
				return;
			}

            int count = 0;
            foreach (var s in args) {

                ConvertFile(args, count);
                count++;
            }
		}

        public static void ConvertFile(string[] args, int count) {

            FileInfo file = new FileInfo(args[count]);
            if (file.Extension.ToLower() == ".wem")
            {
                Console.WriteLine("WARNING: WEM conversion is a bit busted right now! If your file is broken, sorry! A patch will be out ASAP.");
                WEMFile wem = new WEMFile(file.FullName);
                WAVFile wav = wem.ConvertToWAV();
                wav.SaveToFile(file.FullName + ".wav");
            }
            else
            {
                file = FFmpegWrapper.ConvertToWaveFile(file.FullName);
                WAVFile wav = new WAVFile(file.FullName);
                WEMFile wem = wav.ConvertToWEM();
                wem.SaveToFile(args[count].Replace(".wav","").Replace(".mp3","") + ".wem");
            }

        }
	}
}
