# Mecha

Mecha is a compact low-level debugger for the League of Legends client. It allows users to inspect, analyze, and profile the client from top to bottom.

# Current Status

As of February 26, 2024, Mecha is temporarily discontinued. 

Due to my on-and-off interest in League of Legends coupled with low motivation among other personal issues, I haven't been able to complete the last iteration of Mecha. I apologize for this.

Currently, I have a few good ideas floating around and I am considering rewriting the project from scratch. This will, however, largely depend on the overall security of the client; with Vanguard being introduced and Riot (potentially) trying to take more measures against client modifications and scripts, changes along the way are inevitable. I cannot promise anything at this time. 

ETA: April or May, 2024

The details of the next iteration of Mecha might look something like this:

### C instead of C++

**Reason:** Only a very small subset of C++ is relevant and used in Mecha; the language adds unnecessary complexity. C will be a better match for the next iteration. 

### Make + gcc instead of Visual Studio + MSVC

**Reason:** The Microsoft toolchain is unintuitive and sluggish, making it a significant time sink. The next iteration will adopt a more traditional setup instead, with Make as the build system and gcc as the compiler.

### Improved configurability

**Reason:** The last iteration of Mecha used an awfully overengineered JSON configuration system. A much simpler INI/TOML-based configuration will be replacing it.

### More user support

**Reason:** Less technical users often found it hard to get Mecha working due to poor naming and incomplete documentation. I will be directing more time towards creating helpful resources. 