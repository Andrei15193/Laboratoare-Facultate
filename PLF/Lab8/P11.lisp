(defun sumaPare(l)
    (cond
        ((and (numberp l) (= (mod l 2) 0)) l)
        ((atom l) 0)
        (t (apply #'+ (mapcar #'sumaPare l)))
    )
)

(defun sumaImpare(l)
    (cond
        ((and (numberp l) (= (mod l 2) 1)) l)
        ((atom l) 0)
        (t (apply #'+ (mapcar #'sumaImpare l)))
    )
)

(defun diferenta(l)
    (-
        (apply #'+ (mapcar #'sumaPare l))
        (apply #'+ (mapcar #'sumaImpare l))
    )
)

