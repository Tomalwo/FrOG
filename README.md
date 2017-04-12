FrOG: Framework for Optimization in Grasshopper
<br>Branch for Visual Studio 2012 (no C# 6)

Developed by Thomas Wortmann and Akbar Zuardin with contributions by Dimitry Demin and Christoph Waibel.

This project intends to make it easier to link optimization algorithms to Grasshopper (http://www.grasshopper3d.com/).
FrOG handles all interactions with Grasshopper and provides a GUI. 

To add a new solver, implement the ISolver interface and add the resulting solver class to GetSolverList, both in SolverInterface.cs.
The solver should appear in the GUI, with different presets of settings listed seperatly.
HillclimberInterface.cs is provided as an example that links Hillclimber.cs to FrOG.

Please feel free to use this code (which comes without any warranties) and to contribute improvements to the Framework.
When using FrOG for academic research, please cite this repository.

Happy optimizing!
