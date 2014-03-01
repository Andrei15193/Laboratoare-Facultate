clc;
numarTermeni = 750000;
eroare = 1e-5;

fprintf('log(2) = %g\n', log(2));
fprintf('ln(1 + x)\n');
fprintf('aprox1=%g (aproximare cu %i termeni)\n', aprox1log(1, numarTermeni), numarTermeni);
fprintf('aprox2=%g (eroare %i)\n', aprox2log(1, eroare), eroare);
fprintf('ln((1 + x)/(1 - x))\n');
fprintf('aprox3=%g (eroare %i)\n', aprox3log(1, eroare), eroare);