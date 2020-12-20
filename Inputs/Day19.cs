namespace AdventOfCode2020.Inputs
{
    class Day19

    {
        public string testInput = @"0: 4 1 5
1: 2 3 | 3 2
2: 4 4 | 5 5
3: 4 5 | 5 4
4: a
5: b

ababbb
bababa
abbbab
aaabbb
aaaabbb";


        public string input = @"17: 72 112 | 71 3
31: 71 33 | 72 57
49: 72 110 | 71 45
37: 71 68 | 72 45
124: 71 128 | 72 64
1: 105 71 | 29 72
44: 72 130 | 71 93
93: 72 45 | 71 105
126: 34 71 | 59 72
89: 71 16 | 72 118
92: 21 71 | 12 72
55: 47 72 | 61 71
7: 29 72 | 29 71
45: 72 72 | 72 71
28: 110 71 | 109 72
10: 110 72 | 63 71
29: 72 71
27: 71 53 | 72 13
107: 45 71
84: 5 71 | 39 72
119: 72 105 | 71 110
53: 28 71 | 10 72
18: 72 86 | 71 71
25: 110 72 | 105 71
82: 106 71 | 75 72
46: 29 71 | 63 72
71: a
129: 49 71 | 80 72
123: 12 71 | 79 72
111: 110 72 | 96 71
35: 72 107 | 71 104
113: 10 72 | 1 71
106: 26 72 | 6 71
8: 42
36: 71 10 | 72 79
26: 72 122 | 71 51
74: 48 72 | 2 71
5: 72 104 | 71 22
120: 71 48 | 72 2
64: 72 96 | 71 56
72: b
131: 63 72 | 56 71
79: 56 71
23: 72 96 | 71 29
6: 71 111 | 72 32
43: 71 73 | 72 70
78: 14 71 | 74 72
16: 71 29
54: 110 72 | 18 71
80: 2 72 | 109 71
75: 108 72 | 30 71
13: 25 71 | 116 72
132: 15 72 | 24 71
70: 114 71 | 113 72
112: 7 71 | 131 72
88: 72 89 | 71 102
130: 71 110 | 72 56
0: 8 11
117: 66 71 | 27 72
127: 72 126 | 71 67
52: 72 109 | 71 96
122: 45 72 | 2 71
103: 72 44 | 71 36
58: 72 41 | 71 35
114: 72 121 | 71 52
85: 72 120 | 71 23
19: 72 18 | 71 68
2: 72 71 | 71 86
65: 54 71 | 90 72
96: 71 71 | 71 72
69: 72 17 | 71 40
116: 71 48 | 72 29
66: 72 100 | 71 92
115: 72 48 | 71 63
39: 72 62 | 71 83
22: 71 96
50: 105 71 | 134 72
108: 87 72 | 46 71
34: 55 72 | 9 71
11: 42 31
14: 56 71 | 96 72
24: 58 72 | 95 71
83: 72 45 | 71 29
47: 72 110 | 71 109
94: 37 72 | 97 71
12: 48 72
51: 72 48 | 71 68
98: 109 71 | 18 72
32: 72 105 | 71 68
95: 72 77 | 71 78
118: 72 68 | 71 2
21: 72 45 | 71 134
59: 71 123 | 72 133
105: 71 71
76: 72 109 | 71 29
56: 71 71 | 72 72
68: 72 86 | 71 72
3: 107 72 | 19 71
86: 72 | 71
33: 72 69 | 71 43
128: 2 72 | 68 71
67: 72 84 | 71 125
60: 29 71 | 110 72
100: 79 72 | 4 71
97: 56 72 | 45 71
133: 76 71 | 122 72
104: 45 72 | 109 71
109: 72 71 | 71 71
30: 60 71 | 4 72
99: 71 50 | 72 38
9: 115 72 | 101 71
87: 68 71 | 63 72
77: 81 72 | 20 71
101: 71 2 | 72 110
20: 71 45 | 72 56
40: 129 71 | 65 72
42: 127 71 | 132 72
91: 71 105 | 72 109
81: 72 45
62: 45 72 | 63 71
61: 71 110 | 72 63
4: 71 134 | 72 134
57: 82 72 | 117 71
41: 71 91 | 72 116
134: 71 72
73: 71 99 | 72 85
90: 63 71 | 2 72
15: 71 103 | 72 88
38: 72 63
121: 134 71 | 96 72
110: 86 86
48: 71 72 | 72 72
63: 72 71 | 71 72
125: 71 124 | 72 94
102: 119 72 | 98 71

abbaabbbaaaaaabaababbabbababaaba
babbbbbbbbababaaaaaababbbabababaabbaaaaabbabaaab
aaaaabaaabbbaabbaaababaa
babababbaaababbbbababaaaaabbaababbbabaaa
bbaaaaaababbaaabaabaabbb
aabbbaaaabbaaabbbabbbaaa
aabbbaabaababbabbbbbbaba
bbbaaabbaaaaabbabbabbaab
abbbbbaababbababbbbbaabaabbababaabbaabaa
aabbabbabbabaabababbbabaabababbb
bbaaabbabbaaabbabaabbaaaaaaaaaabaabaaabbbaabaabb
bbaabbabbabbabaaabbbbabb
aaaabbbbaaaabaabbababaabbbabaaabaabbabab
abbbaabbbbabbaaabaaabbab
bbbabbbaaabbbababbaaabbb
abbababbaaabababbbbbbbab
aabbaaaabbaaaaabaaababbbbbaabaaa
abaaabaaaabbababbabbabbbabbaabbabbaabbbb
abbbbbaaaaaabbaabbbbbbab
babaaaaabbbabbaaabbabaaa
aaabaaaaabaababaaaabbaab
babbababaaaaababaabbaaabbbbbaaababbbaaaaababaaabbabbbbabaabbabababbaabba
bbbbbbbaababbabbaaaaabaabaaaaabbababbbbbbabaabbaabababaaaabbabba
bbbaabbbaaaabababababbbb
babbbbbaabbbaababababbab
bbabaaaaabbbabaaabbbbabb
bbbaabaabaabbaaaaaababbbaababbbaaabababbbaaabbba
abbabababbabaaaaabbbaaab
bbbaaabaaaaaaabbabababbaaaabbbaabaaaaabaaabaaaaa
aabbabbabbaababbbbaababbbbabaaaaaabbabbabbaaabbbbaaabaabbabbbbbb
bbababbaababbabababbaaababaabaabbaaabbabababaaabbabbbbba
baaababbbbabbbbababbaaababbaaabbbbbaaaaaabaabbabbababbba
aabbaaaabaaaabbabbababbb
aaaabbaaaabaabbaaabaabbb
bbbbbbbbababbabaaaaaaaabaabbbabbbabbabbaaaaaabbaababbaabababbbab
aaaaabaaaabababaaabbbbbaabbbbbbbbabbaaaa
aabbbbbaaaabbbbbaabbaabb
abbbbbbbaaabbbbbbaababab
aaababbababbaaababbabbaa
babbbabbbaaaaabbbabababa
bbbaabbbbbbaaaaaaaaaaababbabaaaabbabbaababaaaaba
bbaaaaaabbababaabbbaabaabaaaabaaabaaabbb
babbbabaaaabbbbbbabbaabb
aabaaabbaababababbabbaaaaabababb
babaaabbabaaaaaaabbaababbabababa
bbababaabaaabaaababaababbaaaabbbaaaabaabaabbbaabbbaabbabbbbabbaaabbbbbbbaaaaabbabbbbabaaabbaaabb
aaabaaaaaabbbaababbbbaba
bbbbaaaababaaabbbbabbabb
bbbabbbaaabbbaaabbabbaaabbaabbba
baaaabbaabaaababbbbabaaa
aabbbababbbbbabbaaaaabbbaaabbababaaababbabbbabbb
abaaabaaabaaabaaaaabaaabaababbaa
bbaaababbbbbbbbbaaaaaaaabbbabaabbaaaaabbbbaaaaaabbaaaaaa
bbbaaabbabbbbaabaabbbbbabaababba
babaaaaababbaaabababaabb
aaaaaaabaaaabaabbabaabababababbababababa
aabbaaabbaabaaaaaababbaa
abbbaabaaabbbaababbaaabbbbaaabbbaabaaaaa
bbbbbbbbbaababbbbbababaaababbbaa
babaaaabbbbaabbabbbbbaaaaaaaaaaaabbbbbabaabbbbbb
abaababababaaabbaaabaaba
babaabbbaabbbabaaaaabbab
ababaabbaabbbaaaabbaabbaaaababbbbabaabaabbaabbbabbabbbbbaabbabaa
aabaabbaaaabaaaabaabbbaa
baabbbbbbaababbabbabbabaabbbabbb
abaaaaaababbbbbaababababbbbbbbaaaabbbbabbaabbbbb
aabbbabaaaaabbaabaaaababbbbbaabbaaabbabbbabbbaaaaababbbb
babbabaaaaaaabbababbaabb
ababbabaaabbbbbabbaaaaba
baaabbbbbbbabbbababaabbbbbaabbbb
aaaabaaaaabbbbbbbbbbaababababbaabbbbbbabaabaabaaaaaabbbb
abbababaaabbbbbabbabbbabbbbabbba
abbababaabaababaabababaa
bbaaaabaabbbbababbbbaabbbabaaaaabbabbbaabbaaabababaabbbbbbabbbab
abbbbabbabbabbabbbabbbba
baaaaaaabbbbabbbbaabbaaaaabbbbabbabbbbabababababaababaaa
babbaaabababbbbbaaaaaaaa
abaabbaaaaabaabbababaaaaaaabbabababbbabaaabbabaabbbbbbabbabbaaaabbbbbbbb
aaaabbbbbabbaaababbaaabbabababab
bbbaabaaabbabbbbabbbbbbbbbaabbbbbbabbbbb
abbababbabaabbaabbabbabaaaabbbbbbbabaaabbbbbbbaaabbbaabbababaaaabaabaababababbbbbbbbbabb
abbbbaabababbabbbbbbabbbbbbabbab
baabbaaababaabbbabbbabaaaababbbaababaabababbbaabaababbbbbaabbabb
abaabaabbabbaaaababbbbbbbbabbbaaaabbaaabbbbabbaaabbababbbbbbaaaa
aabaabbabbbbaabababbbaab
ababababbbaaaabaabbbabbbbbabaaab
aabbbabbbaaaaaaaaabaabaaaababaaabaabbbbaaaabaabaaaababaabbaaaaba
bbaaaaaaababaabaabaaabab
bbbbbabaabbbaabaabbbbbbbaabbbaaababaabbbbbbababaabbbbbbbaaaaabbabbbbaaba
bbabbababbaaabaabbbbbbbbaaaababaabbbabbabaaabbbabbbaabab
aaaabaaabbbabbaaaaabbababaabaabb
abbbaaaabbbbaaaabbbbbaab
aaababbabbbbbbbaabbbbbbabaabaaba
abaaaaaaaabaabbaabaaaaaabbababaaaabbabab
aaababbaabbbaabbabbaabbbbbabbbaaabbabbabbbbabbab
aabbaaaaabbbbababababbab
abbaabababbababbbbbabbaabbbbaaab
aaababbabbabaababbabbaab
baabbbbbbbabbbabaabbbbaaaaaababbaaabaaabbabbbaabbbaaaaba
babbaaababbababbbababaaababbabbabbbaabbbbabbbaab
bbbaabbaaaaabaabababbaab
bbbbbabaababbaabaababbbbaabbabba
baaaaaabbaabbbabaabaabbabaabbaabbbabaabbbabaaaaa
ababaabbbbbaabbbbbbbabbaabbbaaaaabbaabbaababaaabbbbbbabbabaaaabb
bbabbbbbbbaaaabaabababaa
abaabaabaabaabaaabaaaaba
aabbbbababaababaaabaaaaa
baabaabaaaabaabababbbbaababbaaba
bbaaaaaaababbbbbbabababbbababbba
aababaabaabbbabaabbaabbbaababbababbaaaba
ababaaabaaabababbaabbbbaaabbbaab
abbaaaabababbababbabaabb
babbbabbbaaababbaaabbaaaaaabaaabbbabbabb
bbbbabbbbbbbbabbbbaababa
bbbbabaabaabbabbaabbabaababbbbaa
babaaaaabaaabbbbabbaabbbaabbabbbbbabbabb
bbbabbbaaabbbbaabbabaaabaaabbabbbaababbbbbabaababbbaaaabbaabbaab
baaaabbbabaabaaaabbbbaaaabbabaaaaabbabbbbbbbbbbbaabbabbbababababbaaabaabaabbabab
abaabbabbababbbabbbababbbaababab
abbbaabbbaaaababaaaaabbaabbabbbaaabaaaaa
baaaabbaaaaabbbbabbbaaab
ababbabbbbabaaaabaabbabaaabaaabbaabbbaaaaababbaabaaabbbabbbbabba
abaababbbbaaaabaaaaaabbbaaaabababbaaababbabbbbaabbbbaaaaaabaababaabababbaabaababbaababab
abbbaabaaaaabbbbabbbbaabbabbabababbbabaababbbbbaaababbbb
aaaabbaaaabaabbbbabbabbbbabbbabbbaaabbabbaabbaaaabbbabbbabaaaabbbaabbabababbbabbbaaabaaa
aaabbaaabaabbbbbaabbabab
babaabaaaabababaabbabbba
bbbababbbababaaabbbaaaaaaaabaabbaabbbbbbaaabbbab
abaabaaaabaaaababaabbaabbabaabba
baababbbbbbbaababbaababbbbbbbbbababbbaababaaabba
bbbbaaaaaaaaabbaaaabbbaa
abbaababaabbbaaabbbbaaaaaababaabbabababa
babbbabbababbabbbbbbaabb
aabaababaababbbaabbabbbbaaababababaabababbababaababbabbabbbaaaaabababbbabaaaaaaababaabbbabbbabbb
bbabbaaabaaaabaaaaaaabaabbbbbbaabbbabbbaabaabbab
aabbbabbbbaababbabababaababbbbaabbaaabbbaaabaaba
aaaabaabbbbabbaabbbabaaa
baabbabbababbbaabababbbabaaabaab
bbabbbaaaaabbbbbabababbb
aaabaaabbbaaaaabbbaabaaa
abaaabbbabbbbbaaaabbbbabbababaabaabbabbababaabbbabbbbaababbbbaab
babbabbbbaaaabababbbabbababababaabaabbaaaabababbababbbabbbbaabab
bbaabbaaabbbaabbbbabbabb
abbaabbaaabaaaaabaaabbbaabbbaaabaaaaaabb
babaabbbabbbbbabbbbbabba
aabbbbbaabbababbaaabaaabbaababaabaabbbaabbbababa
bbbbbbaaabbbaabbaaaabbaababbbaab
baaaaaaaababbabaabaaabab
abbaababbaabbbababaabbab
abbbabababbabbbbbabaabba
bbbaabbbbbbbabbbaaabaaab
bbbbababbbbaabbabbbbabbbbabbabbabbaabaaaaabbaabb
abbaaabbaaaaabbabbbabbbb
abbbbbbbbabbbababaaaaaaabaaabbba
abbbbbbbbbaabbababaaaaab
bbbbbbaabaaaabaabbbbbbabababaaaa
baabbbbaabbaabababbbaabaabbaaaabbbababaababaaabbbabaaababababaaa
babbbabbaaababbabbbbabbbaaababbbaaaaaaaabbaaabbb
aaabaaaaaabbbbbaaabaabbbbaaabaaa
aabbbabbaaaaabbaabbbaaab
baaabbaabbbaaabaabaaabaaabbbabbb
bbbbbabbbbbaabaabbbbababbbaababa
baaabbaaaabbbaabbaababab
ababbbbaaabbaaababaaabaabbbaaabbaaabbaabaabababbaabbaabb
babaaaaabaababaabbaaabaa
bbabaaaaaaaabbaabbaababa
bbabababaabbababbbaaaabababbaabb
bbbabbaaabbaababbaababba
bbaaabaabbbaaaababaaaaba
aabbbbbbababaabaaabbbbbaaabbaaaaaabaabaa
aabaabaaaabaaaaaaaababaaababababbbaaabbababbaabbbbaaaababbbbabbbaabbbabaaaabbaba
bababbbabaabbbbbabbabaababbaaabaaaaaabbabababaababbbabaaaaaabbbbababbbaa
baaaaabbabbbbbaabbbababbbababbab
aaabaaabbaaababbbbabbbba
babaaaaaababaaababaaabbbbaaaabaaaababbbaaaabbbaaaaaaaababbabbababbbbaabbbabaabab
abbbbbbbaababbababbbbbbbabaabababaabaaab
baabbbbbbabaababaaaabaababaabaabbbaaabaa
bbbabaababaaaabbabbaaaaaaaaabbaabababaaa
babbbbabbbabaabbabbabaabaaababbbaabbbabbaaaabbabbbaaaaaaababbaababaaaaaaaabbbababbbbabbb
aaabaaabbbbbbbaaaaabaabbbaabaabb
aaabbbbbababbabbabbbbbab
aabaabbabaabaabbbbbbbbabbbaabbbb
bbbaabbabbbbabbbbbbaaabaababbababababaabaaaabbba
abbababaabbbaaaabbbbbabbaabaabaababbaabbaaabbbbabbbababa
abbbbaabaaabaabbaaabaaabaaaaaaabaaaaaabaaabaabaaababaaab
ababbababaaabbbbbbabbaaabababbba
aaabbbbbabbaabbbabbbaababaaabbab
bbbbaaaabaababaabbaaabaa
ababbabbabbaaaaaabbaaaabaabbaabbbbabaabb
abbabbbbbbaabaabababaaaaababbbab
baaaaabaaaabbabbaaaaababbaaaaaaaaabababababaaaaaaabaabaaababbbbb
bbabaaabbaaaaabaaababaaaaaabbaababbabbab
baaaaaabbabbbbaabbababbbaaaabbbbaaaaaaaabbababaaaaabbbba
baabbbbbbababaaaabbbbbaaabababbbbbaaabbbaaabbababbaabbbabaaaaaabbabbbaaaaabbaaba
aaabbaaaabbaabbbbbabbabb
aaabaabbababbabbaababaaa
bbbbbaaaaabbbaaabaabbbabaabbaaba
babaabbabbbbbbabababbbababaaabbabbbbbaba
bbbaaaaaabbaaabbbabaaaabbbbabaaa
aaaababbbabaababaaababab
aaabbaaaaabbbaabbbabaaab
abbbbaaabaabbaaaaabbbabaaaabaaaabbbababbababbabababbbbaabbbbaababbbaaaaa
bbaaaaaababbabaaababbaaaabababba
bbabbbaabbbabaabbbaaabaa
aabaaabbaabbbababbabaaaaaaabbbabaabbabaa
baaaaabbbbbbababbbabbbbbabaaabbbbbaaababbababbbaabababaaabbbabbbaaababba
babbbbbbababbaaaaabbabbaaabbababbbabaaabaabbbabaabaaababbabbabbabaaaaabbbaaabbbb
abbbaabbaabbbaabbabbbbba
abaaaabbaaabaaaaaabbabab
aaaaabbbbabbbabbbaababbababbaabababbabaababbaabbbbaaaababbabbabbaabaabaabbbbbaaaaabaaababbbbbaaa
bbababbbbaaababababbbbab
bbabbbabbbaaaaaaabbaabaa
bbbaaababaaababbbbbabbaaaabbabbaaaaaaaababbbaaabbaabbabbaaaaaaabaaaaaabb
bbbbbabbababbabbbbbaaaaaaaabbababababababbababab
ababbbbbaaababbaabaaaaaaabaaaaaabbbbbbba
babbabbabaaabbbbbbaaabab
bababbaabbaaaaabaaaaaabb
bbbbbbbbabbaabbaabaabaaababaabaabaaaabbbaabbaaba
baabbbbbbbbabababaabbaabababbbbbabbabaabbabbababaaabbbaa
bbabbbaabbbbaaaaaabbabab
aaaabaababbbbbbaaabbbabbababaabb
aabbbbbaabbbbabaababaaab
bbbaabaaaaabaaabbbabbbba
babbaaabaabababbbababaaaabbabbabbbbabbba
abaaaababaabbabbababaaaa
bababaaaababbabbaabbaabb
baaabbaaabbaababbbaaabab
bbbbababbababaaabbbaabbaababaaba
abaabaaaaaabbbbbbbbbabbabababbbaababbaabbbabbababbaaababbbbabbbbbbabaaabaabababbababbbab
aaaaaabaaababaabbbabbbbb
abbbbbbbbaaaaabbbbaaaabb
baabbbbaabbaaabbbaaababa
baaabaaaabbaabaabbaaaaba
bbbbbaababbaaaabbaaababbabbbbbaaaaabbbabaababababaaaabbaaabaaaabbbabbbaa
bbbbababbbabbaaabbbbaaababaabbababbbaaab
aaabbaaabaaababbabaaabba
babbabaaaaaabbaaabbabaab
babbbbbaaaabbbabaabaababaabaaaabbaabbaaaaabbabbb
abbbbababbbbbababaaabaaabbbabaabbaabababbabbabaabbaaabbbaaaabbaaabaaaabaabbbaabb
baabbbbbbbbababbbaababaaaaaabaababbabaab
babbabbaabbbbbaaabbbaabbbbaabbbb
baaabaabbaaabbbbaaababaabbbababbaaaaabaabbaaabbaaabbababababbbba
aabaabbabbaaabbabaabaabb
bbbaabbababaaaaabbbbbbbaaabbaaba
aaabbaaabaababbbababaabb
bbabbbaabbaaababababababbbbbbabaaabaabbbaabaaaabababaabb
abbbaabaabbababaababbaba
bbababaaaabbbaaabaabbabb
ababbabababababbbbaababa
aabaaabbaababbabbabbbaaa
aaaaababaabbbbaabbbaaaab
aaaabbbbbbaaabbababbbbaabaaabaaababbbbaaababbbaabaabbbab
abaababaabbbaaaabbbaaaba
aaabbaabaaabbbababbbabbbbababbba
baaabbaaabbababbbbbbabab
bbaabbbbaaababababbaaaabbaabbaabaababbaa
bbbaabbaaaaaabbbbbabaaabaabbbaaabbaabaaaabbaabababbbabbb
bababbaaabbaaabbbbbababbbaabaaba
baaaabbbbabaaabbbbaabbbb
aabbaaaabbbbaaababaaaaba
ababbbbbabbaabbbbbbbbaaaaaaaaaababbaaabbaabbaabbbbaabbba
abaaaabbbbaabbaaaabbbaba
bbaababbaaabaaaababbaaab
abbbbabbaaababbaabbbbbbbbbbababbabaababbbbbbbabaababbabb
baaababbbaababbbaabbaaba
babbaaaaaabaabaaabaaaaaabaabbbbbababaabaaaababaaaabbbaabaaabaaaa
baabbaaaababaabbaabbbbaaaabbabbbaaaaaaaabbbabbbbabbbaabababababbaabaabaabaabbaaa
ababbbbbabbabbbbaababbbaaabababaababbbabbaaabaaa
babbabaababbbabbbbbbaababaabbabb
abaabbaababaaaababaabbabbaaaabaaabbabbaa
baabbbbbaabbbabaaabbbbababbbaababbbbaabbabaaaaba
bbbbbaababaaabaaaabaababbaaaaabbbabbbaabbaabbaabbbaababa
bbaaaaabbbabaaaaabbababbbaabbababbabbbba
aaabaaabbaababbbbbbaaabaaababaabbbaabbbbabbabaaabaababbbaaaabaaabaaaaaba
baabbababbabbabbabababbababbaabbbbabaaabbbbabbbb
abbabbbbbbabaabaaaaababbaaabbbba
abaaaaaabbbbbbaaaaabbabaabbaaabbbbbbbabbaaababaa
bbabaabababbaaababbabaab
aabababbaabaaabaaabaaaab
abbaabbbababbbbabaabbaab
aabaaabaabaaabaaaaaabbbaabbabababaabbbbbaaaaabbabababbabbaaabbaabbbaaaaaabababab
bbbbaababaaaabbabbaabaaa
babbbabababbabaaaabaabbb
abbbbbaaaaababbbbababbbb
babbaabbbabababbaabbababbbabababaabaabaabaaabaab
babbabbaabbbbbbabbababaababaabba
abbabbabbbaabaababbababbbbabbababaabbbbb
aabbababbaabbbabbbababab
aaaaabbaababbbbaaabbaaba
bbbbbaaabaaaabbababbaaabbaaabbbbbbbbaaabaaabaabb
bbaababbaabbbaaabbbaabbabbaabbaababaaabbababbbaa
abaaabbbbbababbbbbaababa
bbbbbabbbaababaaabbbaabbbababbab
abbababaabbbbbbaaaaaababaabbbbaaabbaabbabbaaabaa
babbabaaaabaaabaabbabaabababaaab
aaababababbaabaabbababbabbbababbaabbaaabbabaaabb
ababbaabbabbbbaaababababbaaaabaaaabbabaa
bbbaabbbabbabababaaaaaba
aaaabaaaabaaaaaaaababbbb
bbbaaabaaaaabaaabbaaaaba
bababaaabbaabbabbaaabaaa
abbbabbababaaabbbbbaaabababaabbbababbaaa
aabbaaabbbbabbaabbababbb
abbbababbbaaaaabaaaaabbbabaabbba
bbbbabbbaaabbaaaaabaabababbabaabbabbbbbb
bbbbbaaabaaaabbaaabbbaaabbbbababbaaaaabb
babbaabbaabaabbaabbbabaaaaabaaaa
ababbababbbaaababbbaabaabbbbbaabaaaaabbbbaababbabbabbbbabbbaabab
aabbbaabbbaababbbaaabbab
baabaaaababaaaababaabbbb
aaaababaabaabababbaaabab
baabbabaabbbababbbbbbaaaabbbababbbaaaabb
bbbabaaabbbbbaaabbbbaabbbbbabbabbbaababababababa
aaaabbbbbaaaaabbbabbabbababbabbb
babaabbbbaabbabababaaaaaaaaaaaaa
abbabababaabbababbabbbbb
aaabbbbbbabbabbaababbaaa
babaabaaaabbbaaabbbaabba
bbbabbababbabaabbaaaaaabaababbaaabaabbbabbaaabbaaabaabaabbabbaababbbaabbbaaaababbbaaabbbbbabbbab
ababbbbbaaaabaabaaababab
abbaaabbaaaaabaabaababab
bbbbbaaaabaaabaaaaabbabaababaaaa
baabaaabbbbbbabbbabbabba
bbbbbabbaaabaaabaaabbabb
aabbaaaaabbbbbbabbaaaaababbaaaba
abaaaaaaaababaababbbabaa
bbbaaabaabaabaababbabbba
aaaabbbbbaaabbbbaabbbbaaabaabaaa
abbbabaaaabbbbabaaaaaaabbbbbaaaabaaaabbbaaaaabbabbaaabaa
aaabbbbababbbbaabaabaababbbbaabbbaabbaab
baaaaaaababaaabbbababbbb
aaabbabaabbbbbbaaabbaabb
aababbabbbbbbbbaaaababaa
aabbaaaaaaaaaabaabbbababaababbbb
abbabbbbaababbbaaabbbabbaaabbabaaaaabbab
baababaaaababbbababbbaaa
abbaaaaaaaabbbbbaababbab
abaaaabbabbbaabbabaabbbb
aaababbabbbbabbbaabaabbb
baaaabbbbbbabaababababba
abbbbbbaabbbabaaabaaaaaaaabbaabaaabbabbbbbbbabbabbbaaaabababbbabbbbabbbb
baababaababbbabaababaabb
abaaabaabbaabbabbabaabbbaabaaaba
aaaabbabaabbbbabaaabaabbaabaabbaabbbabba
bababbaabbbaaabbbbaabbaabaababaabaaababaaaabababbaabbaab
babbabbabaaaabaabababbba
aaabaaaababaababbaaabbba
baabaabbaaabbabaababababaabbaaaabababbab
baaabbbbbaabbabaaabbbaab
aabbabaabaaabbaabbaaaabaabaaaabbbbabaabaaaabbbbaaaabbaabbabbbbaa
aaaaaaaabbbababaabbbbaba
abbbabbaaaabaaabbbaaaaaabbbbabbbabbbababbaabaabaabbabbaabaabaabaabaaabbb
abbbabaaaabababaabbabbbbabbbabbb
bbbbbbbbababbbbaaabaaaba
aabbaaaaaababababaaaabbabbbbaabb
babbbabbbbbaaababababbbb
bbbbbaaababaabbbababbbaabababaab
abbbbaabbbbaaaaabbaababbabaabaaa
abbbababbbaabbabbbbbabba
abbbabaababbbabbabbbbaaa
bbabbabababaaaabbaaaabbbbaaaaaabbaabbbbaaabbbbbabaabbbba
abbbababbababbaabbaababbaaaaabbbbaaaaaabbaabbbba
bbabaabbbbbbbbabbaabbbabbaabbaabaababbbb
aaaaabaabaababbbabbaababbbbaaabaabbaaaab
bbbbbbbbaaaaaabbaababbaaababaaab
abbbabaaaaaabbaabbbaaaab
bbababaaabbbaabaabababab
aaababbabbbbbaabbbbbaabb";
    }
}