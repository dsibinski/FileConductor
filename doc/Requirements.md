#Requirements
`FileConductor` should be working as a Windows Service application executing its tasks in the background. Its purpose it to be able to monitor several directories (local/remote) at a time and process the files appearing in those directories according to the rules specified.
Such tool could be very useful in any kind of integration of developed system with any external system/platform/tool- very often such integration is realized by exchanging text files in various formats. Each endpoint in this process implements a mechanism which takes such file and processes it, adding corresponding object in the destination system. System also implements the opposite mechanism, that allows to analyse data in its current database and export it into the text file, allowing system on the other side to import it later.

The purpose of `FileConductor` is only to help in transferring (`conducting`) the files between both sides. Files are sometimes transferred from local folders to other local folders, or to/from remote servers like FTP/SFTP. This document described the requirements we would like `FileConductor` to meet.

1. Communication protocols supported:
  a) local (netbios),
  b) FTP,
  c) SFTP,
  d) FTPS,
  e) ???

2. Possible communication ways:
  a) Local - Local,
  b) Local - Remote,
  c) Remote - Local.

3. Scheduling (independent from time zones/time changes):
  a) Interval (every x seconds, minutes, hours etc.),
  b) Fixed hours during the day,
  c) Only on particular days of the week,
  d) Only on particular days of the month.

4. Files management policies supported:
  a) Always download all files from source catalogue,
  b) Download only the files which have changed since the last download,
  c) After downloading the file (all those should be optional/configurable):
    - execute an SQL procedure giving in the path to file downloaded (after moving to destination catalogue),
    - do nothing,
    - delete from source catalogue,
    - change file's name in the source catalogue,
    - move the file to a different catalogue on source server,
    - do not archive downloaded files locally - each process which actually takes and processes the file should decide whether the file is archived or not - `FileConductor` only helps in transferring the files.
  d) After uploading/exporting the file:
    - files exported from local catalogues should always be archived in some subcatalogue (name of the archive folder should be strictly defined?),
  e) Types of files to download/monitor:
    - with any extension,
    - only with specified extension(s).
5. Notifications/errors handling:
  a) allow to send email notifications about connection errors (problem with SQL connection, with FTP/SFTP connection, filesystem actions issues etc.) - different email details should be possible to be provided for each folder watched,
  b) optionally allow to send email notifications about every successful import/export process.
6. Acceptance/verification tests:
  a) Define set of acceptance tests scenarious the application should met,
  b) Mocking the SFTP/FTP servers? Preparation of scripts uploading/downloading several files from them?
7. GUI:
  a) for the beginning, focus only on the functionalities of the Windows Service app, without providing any graphical interface,
  b) in the end, if the solution is stable, think about implementing a GUI configurator tool / installator (?).
