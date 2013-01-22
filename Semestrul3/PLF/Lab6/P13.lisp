(defun aspectDeMunte(l)
    (null (eliminaPrimeleDescrescatoare (eliminaPrimeleCrescatoare l)))
)

(defun eliminaPrimeleCrescatoare(l)
    (cond
        ((null l) nil)
        (T (eliminaPrimeleCrescatoare_aux (car l) (cdr l)))
    )
)

(defun eliminaPrimeleCrescatoare_aux(e l)
    (cond
        ((null l) nil)
        ((> e (car l)) l)
        (T (eliminaPrimeleCrescatoare_aux (car l) (cdr l)))
    )
)

(defun eliminaPrimeleDescrescatoare(l)
    (cond
        ((null l) nil)
        (T (eliminaPrimeleDescrescatoare_aux (car l) (cdr l)))
    )
)

(defun eliminaPrimeleDescrescatoare_aux(e l)
    (cond
        ((null l) nil)
        ((< e (car l)) l)
        (T (eliminaPrimeleDescrescatoare_aux (car l) (cdr l)))
    )
)

(defun maxim(l)
    (cond
        ((null l) nil)
        ((null (cdr l)) (car l))
        ((> (car l) (maxim (cdr l))) (car l))
        (T (maxim (cdr l)))
    )
)

(defun atomiNumerici(l)
    (cond
        ((null l) nil)
        ((numberp (car l)) (cons (car l) (atomiNumerici (cdr l))))
        ((listp (car l)) (append (atomiNumerici(car l)) (atomiNumerici(cdr l))))
        (T (atomiNumerici (cdr l)))
    )
)

(defun eliminaAtom(e l)
    (cond
        ((null l) nil)
        ((EQ e (car l)) (eliminaAtom e (cdr l)))
        ((listp (car l)) (cons (eliminaAtom e (car l)) (eliminaAtom e (cdr l))))
        (T (cons (car l) (eliminaAtom e (cdr l))))
    )
)

(defun eliminaMaxim(l)
    (eliminaAtom
        (maxim
            (atomiNumerici l)
        )
        l
    )
)

(defun cmmmc(a b)
    (cmmmc_aux a b b)
)

(defun cmmmc_aux(a b n)
    (cond
        ((= (MOD b a) 0) b)
        (T (cmmmc_aux a (+ b n) n))
    )
)

(defun cmmmcLista(l)
    (cond
        ((null l) nil)
        ((null (cdr l)) (car l))
        (T (cmmmc (car l) (cmmmcLista(cdr l))))
    )
)

(defun cmmmcListaNeliniara(l)
    (cmmmcLista (atomiNumerici l))
)

(defun produsPare(l)
    (cond
        ((null l) 1)
        ((listp (car l)) (* (produsPare (car l)) (produsPare (cdr l))))
        (
         (and
             (numberp (car l))
             (= (MOD (car l) 2) 0)
         )
         (* (car l) (produsPare (cdr l)))
        )
        (t (produsPare (cdr l)))
    )
)

