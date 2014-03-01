(defun stanga_aux(l nv nm)
    (cond
        ((null l) nil)
        ((= nv (+ nm 1)) nil)
        (T
            (cons
                (car l)
                (cons
                    (cadr l)
                    (stanga_aux (cddr l) (+ 1 nv) (+ nm (cadr l)))
                )
            )
        )
    )
)

(defun stanga(l)
    (stanga_aux (cddr l) 0 0)
)

(defun dreapta_aux(l nv nm)
    (cond
        ((null l) nil)
        ((= nv (+ nm 1)) l)
        (T (dreapta_aux (cddr l) (+ 1 nv) (+ nm (cadr l))))
    )
)

(defun dreapta(l)
    (dreapta_aux (cddr l) 0 0)
)

(defun inordine(l)
    (cond
        ((null l) nil)
        (t (append
                (inordine(stanga l))
                (cons
                    (car l) (inordine(dreapta l))
                )
           )
        )
    )
)
