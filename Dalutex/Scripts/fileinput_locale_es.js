/*!
 * FileInput Spanish (Latin American) Translations
 *
 * This file must be loaded after 'fileinput.js'. Patterns in braces '{}', or
 * any HTML markup tags in the messages must not be converted or translated.
 *
 * @see http://github.com/kartik-v/bootstrap-fileinput
 *
 * NOTE: this file must be saved in UTF-8 encoding.
 */
(function ($) {
    "use strict";

    $.fn.fileinput.locales.es = {
        fileSingle: 'archivo',
        filePlural: 'archivos',
        browseLabel: 'Buscar &hellip;',
        removeLabel: 'Remover',
        removeTitle: 'Limpiar archivos seleccionados',
        cancelLabel: 'Cancelar',
        cancelTitle: 'Abortar el cargue en curso',
        uploadLabel: 'Cargar Archivo',
        uploadTitle: 'Cargar archivos seleccionados',
        msgSizeTooLarge: 'Arquivo "{name}" (<b>{size} KB</b>) excede o tamanho máximo permitido de <b>{maxSize} KB</b>. Por favor selecione outro arquivo!',
        msgFilesTooLess: 'Usted debe seleccionar al menos <b>{n}</b> {files} a cargar. Por favor reintente su cargue!',
        msgFilesTooMany: 'El número de archivos seleccionados a cargar <b>({n})</b> excede el límite máximo permitido de <b>{m}</b>. Por favor reintente su cargue!',
        msgFileNotFound: 'Archivo "{name}" no encontrado!',
        msgFileSecured: 'Restricciones de seguridad previenen la lectura del archivo "{name}".',
        msgFileNotReadable: 'Archivo "{name}" no se puede leer.',
        msgFilePreviewAborted: 'Previsualización del archivo abortada para "{name}".',
        msgFilePreviewError: 'Ocurrió un error mientras se leía el archivo "{name}".',
        msgInvalidFileType: 'Tipo de arquivo inválido para o arquivo "{name}". Apenas arquivos "{types}" são permitidos.',
        msgInvalidFileExtension: 'Extensão de arquivo inválido para "{name}". Apenas arquivos "{extensions}" são permitidos.',
        msgValidationError: 'Erro ao Carregar Arquivo.',
        msgLoading: 'Cargando archivo {index} of {files} &hellip;',
        msgProgress: 'Cargando archivo {index} of {files} - {name} - {percent}% completado.',
        msgSelected: '{n} {files} seleccionados',
        msgFoldersNotAllowed: 'Arrastre y suelte únicamente archivos! Se omite {n} carpeta(s).',
        dropZoneTitle: 'Arraste e solte o arquivo aqui &hellip;'
    };

    $.extend($.fn.fileinput.defaults, $.fn.fileinput.locales.es);
})(window.jQuery);
