function controlLetra(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true; // para la tecla de retroseso
    else if (tecla == 0 || tecla == 9) return true; //<-- PARA EL TABULADOR->su keyCode es 9 pero en tecla se esta transformando a 0 asi que porsiacaso los dos
    patron = /[^0-9]/g, '';
    te = String.fromCharCode(tecla);
    return patron.test(te);
}

function validarPunto(e) {
    obj = e.srcElement || e.target;
    tecla_codigo = (document.all) ? e.keyCode : e.which;
    if (tecla_codigo == 8) return true;
    patron = /[\d.]/;
    tecla_valor = String.fromCharCode(tecla_codigo);
    control = (tecla_codigo == 46 && (/[.]/).test(obj.value)) ? false : true
    return patron.test(tecla_valor) && control;

}
function validarComa(e) {
    obj = e.srcElement || e.target;
    tecla_codigo = (document.all) ? e.keyCode : e.which;
    //alert(tecla_codigo)
    if (tecla_codigo == 8) return true;
    patron = /[\d,]/;
    tecla_valor = String.fromCharCode(tecla_codigo);
    control = (tecla_codigo == 44 && (/[,]/).test(obj.value)) ? false : true
    return patron.test(tecla_valor) && control;

}
function validarNumeroEn(e) {
    tecla = (document.all) ? e.keyCode : e.which;
    if (tecla == 8) return true; // para la tecla de retroseso
    else if (tecla == 0 || tecla == 9) return true;
    patron = /[0-9\s]/;// -> solo numeros
    //-> solo letras   /[^0-9]/g, '';
    te = String.fromCharCode(tecla);
    return patron.test(te);

}