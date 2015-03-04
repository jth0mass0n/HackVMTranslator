﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackVMTranslator
{
    public class Parser
    {
        private System.IO.StreamReader _sr;
        
        public string CurrentCommand { get; set; }
        public bool HasMoreCommands { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public HackVMTranslator.Enums.Enumerations.CommandType CommandType { get; set; }
        
        

        public Parser(string fileName)
        {
            //open the file string 
            _sr = new System.IO.StreamReader(fileName);
            HasMoreCommands = !_sr.EndOfStream;
        }

        private void Clear(){
             CurrentCommand = "";
             Arg1 = "";
             Arg2 = "";
        }   


        public void Advance()
        {
            Clear();//clear fields before proceeding

            CurrentCommand = _sr.ReadLine();

            //handle commented lines  and white space
            if (string.IsNullOrWhiteSpace(CurrentCommand))
            {
                CurrentCommand = "";
            }
            bool isComment = CurrentCommand.Contains("//");
            while (isComment || string.IsNullOrWhiteSpace(CurrentCommand))
            {
                CurrentCommand = _sr.ReadLine();
                if (string.IsNullOrWhiteSpace(CurrentCommand))
                {
                    CurrentCommand = "";
                }
                isComment = CurrentCommand.Contains("//");
            }

            HasMoreCommands = !_sr.EndOfStream;

            ProcessCommand(CurrentCommand);

           
        }

        private void ProcessCommand(string command)
        {
            if (command.Contains("add") || command.Contains("sub") || command.Contains("neg") || command.Contains("eq") || command.Contains("gt") || command.Contains("lt") || command.Contains("and") || command.Contains("or") || command.Contains("not"))
            {
                CommandType = Enums.Enumerations.CommandType.C_ARITHMETIC;
                Arg1 = command;

            }
            else if (command.Contains("push"))
            {
                CommandType = Enums.Enumerations.CommandType.C_PUSH;
                Arg1 = command.Split(' ')[1];
                Arg2 = command.Split(' ')[2];

            }
            else if (command.Contains("pop"))
            {
                CommandType = Enums.Enumerations.CommandType.C_POP;
                Arg1 = command.Split(' ')[1];
                Arg2 = command.Split(' ')[2];
            }

        }

       public void GarbageCollection(){
           _sr.Close();
       }
    }
}