<a href="README.md" style="color: #2079C8;">Introduction</a> | <a href="installation_guide.md" style="color: #2079C8;">Download + Installation Guide</a> | <a href="features.md" style="color: #2079C8;">Features</a> | <a href="https://github.com/Special-Niewbie/HandleOS/discussions" style="color: #2079C8;">Discussions</a> | <a href="BugFix_and_NewFeatures.md" style="color: #2079C8;">Bug Fix & New Features Scheduling</a> | <a href="BCDFix.md" style="color: #2079C8;">Guide to Restoring BCD</a>

<h1>Bug Fix & New Features Scheduling</h1>

<p>
  1. This section is dedicated to tracking and managing approved ideas that we plan to implement. It serves as a roadmap for upcoming features and improvements for HandleOS. 
</p>

<p>
  2. In addition, this page includes a detailed list of bugs that need to be fixed. We aim to address these issues promptly to enhance the overall stability and performance of HandleOS.
</p>



| <img height="30" src="Git_assets/NewFeatures.png">    NEW Features to implement | Status |
| ------------------------------------------------------------ | :----: |
| 1. ***New OS Version 23H2v2: Enhanced Stability & Performance with Console2Desk v2.0.0***<br/>***Impact:*** <br/>- Improved overall system stability and performance.<br/>- Added new features to Console2Desk for enhanced functionality.<br/>- Fixed various bugs and issues.<br/>- Additional improvements and updates. |   ✅    |
| 2. **Add C-State Enable/Disable in Console2Desk**<br/>***Description:*** Simplify the process of enabling and disabling C-State in Console2Desk to allow users to manage power-saving features more effectively.<br/>***Impact:*** This feature will provide users with a convenient way to toggle C-State settings, improving control over power management and potentially enhancing system performance during gaming or high-performance tasks. |   -    |
| 3. **Implement Custom Window Switching Commands in HotKeys4Console2Desk**<br/>***Description:*** Introduce a feature that allows users to switch between windows using customizable commands in HotKeys4Console2Desk. |   ✅    |
| 4. ***Divide 15 Resolution Application into Two Buttons***<br/>***Description:*** Separate the existing 15 Resolution Application feature into two distinct buttons: one for 16:9 screens and another for 16:10 screens.<br/>***Button 1:*** Rename the current resolution application button to clearly indicate it is for 16:9 screens.<br/>***Button 2:*** Introduce a new button that applies resolutions specifically for 16:10 screens. |   ✅    |
| --------                                                     |        |
| --------                                                     |        |
| --------                                                     |        |



-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



| <img height="36" src="Git_assets/Bugs0.png">   BUGS to fix   | Status |
| ------------------------------------------------------------ | :----: |
| 1. **15 Resolution Application Issue**:<br />***Description:*** When applying the 15 resolutions feature, it may not take effect if the operating system is installed with a docking station connected via HDMI, or if HDMI is connected during installation. The primary display in the registry is numbered as the second attached monitor, even though it remains the system's primary monitor.<br/>***Impact:*** Users might experience issues with the resolution settings not being properly applied due to this misidentification of the primary display. |   ✅    |
| 2. **Legion Go Control Command Issue:**<br />***Description:*** Seems, there is a bug involves problem with game commands potentially caused by the control combinations for Legion Go (as Rog Ally it's not effected).<br/>***Impact:*** Users may encounter issues with game controls by `HotKeys4Console2Desk`, which need to be investigated to determine if the Legion Go combinations are causing the problem. |   ✅    |
| --------                                                     |        |
| --------                                                     |        |
| --------                                                     |        |
| --------                                                     |        |