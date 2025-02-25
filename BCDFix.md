<a href="README.md" style="color: #2079C8;">Introduction</a> | <a href="installation_guide.md" style="color: #2079C8;">Download + Installation Guide</a> | <a href="features.md" style="color: #2079C8;">Features</a> | <a href="https://github.com/Special-Niewbie/HandleOS/discussions" style="color: #2079C8;">Discussions</a> | <a href="HybridvsConsole.md" style="color: #2079C8;">Hybrid Mode vs Console Mode</a>| <a href="BCDFix.md" style="color: #2079C8;">Guide to Restoring BCD</a> | <a href="TP.md" style="color: #2079C8;">Tweak Paradise</a> | <a href="PrivacyPolicy.md" style="color: #2079C8;">Policy Privacy</a>



# BCD Fix: Restoring BCD After Applying `Optimize Memory and Reduce Stutter by BCD`

**Important:** If you have applied the `Optimize Memory and Reduce Stutter by BCD` function and are experiencing boot issues, follow these steps to restore your system. Please note that if BitLocker is active or the disk is encrypted, you will need to decrypt it first, which will complicate the process.

## Steps for Restoration

1. **Insert Installation USB and Keyboard:**
   - Plug in the HandleOS installation USB and connect a keyboard.
   - Boot from the USB (ensure Secure Boot is disabled).

2. **Open Command Prompt:**
   - When you reach the HandleOS language selection screen, press `SHIFT + F10` to open Command Prompt.

3. **Identify Backup Location:**
   - In Command Prompt, type `diskpart` and press Enter.
   - Then type `list volume` and press Enter to see the list of volumes.
   - Identify the drive letter of the volume that contains your HandleOS installation (it might be different from `C:` due to the external boot). Note this drive letter.
   - Type `exit` to close Diskpart but keep Command Prompt open.

4. **Import the Backup BCD:**
   - If your HandleOS drive remains `C:`, type the following command and press Enter:
     ```bash
     bcdedit /import C:\Users\HandleOS\AppData\Roaming\Console2Desk\BackupBCD\bcdbackup.bin
     ```
   - If the drive letter has changed, adjust the command to reflect the new letter. For example:
     ```bash
     bcdedit /import YOUR_LETTER:\Users\HandleOS\AppData\Roaming\Console2Desk\BackupBCD\bcdbackup.bin
     ```
   - Ensure you type the command exactly as shown, including spaces.

5. **Restart and Re-enable Secure Boot:**
   - Close all windows by clicking the X button on each.
   - Close Command Prompt and the language selection window.
   - The system should restart. Once it powers off, remove the USB drive.
   - Reboot into BIOS and re-enable Secure Boot.
   - Restart the system again. Your HandleOS should now function as before.
