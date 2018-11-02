## Usage
### Setting range
The basic range box format:
```
Min/Max
```
Replacing "Min" and "Max" with actual numbers of course.
Not all values have to be filled out though. If Min is left blank, it is assumed to be 0. If Max if left blank it is assumed to be the largest number possible within the length. That being said you require either a length value or a max value to be set.
Possible ways to generate number between 0 and 9999 with a length of 4.
```
0/9999
/9999
Setting the length option to 4
```
Length is prioritised over range. Meaning that no numbers will be generated larger than the greatest number possible for a given length
For example, with range of 0/1000 and a length of 3 set, the last number generated will be 999

## Problems
* It's pretty slow, and there are no threads. Generating values from 0/99999 takes around 35.5 seconds for my computer.