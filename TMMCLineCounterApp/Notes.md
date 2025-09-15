# Notes

1. Why the alogithm in CountBlackLines is chosen:
- Initially my approach/algorithm determined black pixels with simple brightness threshold. It used average of RGB to allow for minor JPG/JPEG compresson artifacts. I decided to implement a different algorithm that better fits this assignment.
- Since the assignment's images are MS Paint black lines on white background, it produces non-grayscale lines that are pure black (0, 0, 0) and pure white background (255, 255, 255). However JPG introduces slight compression.
- With my current approach - I am checking all channels individually ( R, G, B < 32 ) that ensures only true dark pixels count as black.
- Requires adjacent columns have black pixels in both the top and bottom halves - It avoids false positives from isolated dark-grey compression spots or noise.
- It merges contiguous columns so thick lines count only once.

2. Performance
- O(WxH)
- An alternative algorithm that I initially implemented worked fine for small-medium images; for very large ones, Lockbits would speed up.

3. Edge Cases
- Handles thick lines by merging adjacent columns.
- Rejects isolated noise because line must appear in both top and bottom halves.
- Robust to JPEG compression, unlike strict ==0,0,0.

4. Testing (outputs, as expected). 
- img_1.jpg -> 1
- img_2.jpg -> 3
- img_2.jpg -> 1
- img_2.jpg -> 7


Additional notes:
The repository can be cloned from my Github repo using the link shared via email.
All commits and commit messages can be viewed on Github.