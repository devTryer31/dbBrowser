﻿using dbBrowser.Commands.Base;
using System;

namespace dbBrowser.Commands
{
    public class LambdaCommand : Command
    {
        private readonly Func<object, bool> _CanExecute;

        private readonly Action<object> _Execute;

        public LambdaCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _CanExecute = canExecute;
            _Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }


        public override bool CanExecute(object parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object parameter)
        {
            if (!_CanExecute(parameter)) return;

            _Execute.Invoke(parameter);
        }
    }
}
