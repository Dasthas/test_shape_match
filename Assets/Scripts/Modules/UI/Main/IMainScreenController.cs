using System;

namespace Modules.UI.Main
{
    public interface IMainScreenController
    {
        Action OnRestartClicked { get; set; }
    }
}