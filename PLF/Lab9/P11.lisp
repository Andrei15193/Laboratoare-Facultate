(defun cmmmc(a b)
    (do
        (
            (c b (setq c (+ c b)))
        )
        (
            (= (mod c a) 0)
            (abs c)
        )
    )
)

(defun cmmmcLista(l)
    (do
        (
            (c (car l) (setq c (cmmmc c (car lista))))
            (lista (cdr l) (setq lista (cdr lista)))
        )
        (
            (null lista)
            c
        )
    )
)

(defun suma(l)
    (do
        (
            (s 0)
            (lista l (setq lista (cdr lista)))
        )
        (
            (null lista)
            s
        )
        (and (numberp (car lista)) (setq s (+ s (car lista))))
    )
)

