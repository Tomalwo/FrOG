FrOG: Framework for Optimization in Grasshopper
Developed by Thomas Wortmann and Akbar Zuardin with contributions by Dimitry Demin and Christoph Waibel.

This project intends to make it easier to link optimization algorithms to Grasshopper (http://www.grasshopper3d.com/).
FrOG handles all interaction with Grasshopper and provides a GUI. 

To add a new solver, implement the ISolver interface and add the resulting solver class to GetSolverList, both in SolverInterface.cs.
The solver should appear in the GUI, with different presets of settings listed seperatly.
HillclimberInterface.cs is provided as an example that links Hillclimber.cs to FrOG.

Happy optimizing!
