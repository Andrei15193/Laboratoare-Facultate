#ifndef UTILE_H
#define UTILE_H

// See Win data types doc: http://msdn.microsoft.com/en-us/library/windows/desktop/aa383751(v=vs.85).aspx
#ifdef UNICODE
    #define PctstrCopy(s1, s2) wcscpy(s1, s2);
    #define PctstrCat(s1, s2) wcscat(s1, s2);
    #define PctstrLen(s) wcslen(s);

#else
    #define PctstrCopy(s1, s2) strcpy(s1, s2);
    #define PctstrCat(s1, s2) strcat(s1, s2);
    #define PctstrLen(s) strlen(s);
#endif /* UNICODE */

#endif /* UTILE */
