### Fake 3ds Payment System

⚠️ Warning! This is just a scenario. Not suitable for real use! ⚠️

- After entering the valid card and amount, control is provided through Hares Bank. (3ds and None3ds) If it is None3ds then the transaction is done directly through Hares Bank. If a transaction will be made with 3ds, the card is first verified through Hares Bank. Then, a security code is created for the desired operation using Marten DB. (5 digits) All transactions with card information are recorded.

- Then the HTML page code is returned. Here you are asked to enter the code sent. If the code is correct, Hares Banka delivery is provided.

### Flow Diagram

<img width="1197" alt="image" src="https://user-images.githubusercontent.com/22862224/215616873-1e941022-dbd7-4729-b9de-2adb2d854c56.png">

### Verification Page

<img width="1012" alt="image" src="https://user-images.githubusercontent.com/22862224/215617369-543f42c4-236c-4e27-a6a5-732a144c89a5.png">
